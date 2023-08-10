using AutoMapper;
using BoostProject.Common.Exceptions;
using BoostProject.Common.Validation;
using BoostProject.Data.Context;
using BoostProject.Data.Entities.Messages;
using BoostProject.Errors;
using BoostProject.Services.MessagesService.Models;
using BoostProject.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoostProject.Services.MessagesService;

public class MessagesService : ServiceManager, IMessagesService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly ILogger<MessagesService> _logger;
    private readonly IMapper _mapper;
    private readonly IModelValidator<SendMessageModel> _sendMessageModelValidation;

    public MessagesService(
        IDbContextFactory<AppDbContext> dbContext,
        ILogger<MessagesService> logger,
        IMapper mapper
        ) : base(dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MessageResponse>> GetMessagesBySenderId(Guid senderId)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        await IsUsersExists(senderId);

        _logger.LogInformation("Returning messages for sender (Id: {SenderId})", senderId);

        var data = await context
            .Messages
            .Where(c => c.SenderId == senderId)
            .Include(c => c.Sender)
            .Include(c => c.Receiver)
            .ToListAsync() ?? default;

        var response = data!.Select(_mapper.Map<MessageResponse>);

        _logger.LogInformation("All chats for user (Id: {SenderId}) was returned successfully", senderId);

        return response;
    }

    public async Task<SendMessageResponse> SendMessage(SendMessageModel model)
    {
        await ValidateSendMessage(model);

        var response = await SaveMessageToDb(model);

        return _mapper.Map<SendMessageResponse>(response);
    }

    private async Task<Message> SaveMessageToDb(SendMessageModel model)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var message = _mapper.Map<Message>(model);

        _logger.LogInformation("Adding message model for user (Id: {SenderId})", model.SenderId);

        var entry = await context.AddAsync(message);
        await context.SaveChangesAsync();

        _logger.LogInformation("Message model for user (Id: {SenderId}) was successfully added.", model.SenderId);

        return entry.Entity;
    }

    private async Task ValidateSendMessage(SendMessageModel model)
    {
        _logger.LogInformation("Validating message model for user (Id: {SenderId})", model.SenderId);

        _sendMessageModelValidation.CheckValidation(model);

        _logger.LogInformation("Message model validation for user (Id: {SenderId}) was successful", model.SenderId);

        await IsUsersExists(model.SenderId, model.ReceiverId);

        _logger.LogInformation("User(s) validation was approved");
    }

    public async Task<DeleteMessageResponse> DeleteMessage(DeleteMessageModel model)
    {
        await IsUsersExists(model.SenderId);

        using var context = await _dbContext.CreateDbContextAsync();

        var message = await context.Messages.FirstOrDefaultAsync(x => x.Id == model.MessageId);

        ProcessException.ThrowIf(
            () => message is null, 
            LocalizedErrorsManager.GetMessage(ErrorLabels.Message.MessageContentIsRequired));

        ProcessException.ThrowIf(
            () => message!.SenderId != model.SenderId, 
            LocalizedErrorsManager.GetMessage(ErrorLabels.Message.MessageBelongsToAnotherUser));

        _logger.LogInformation("Removing message model (Id: {MessageId})", model.MessageId);
        // TODO: Может быть можно убрать entry и напрямую обращаться к message ниже в _mapper. NEED TO BE TESTED.
        var entry = context.Messages.Remove(message!);
        await context.SaveChangesAsync();

        var response = _mapper.Map<DeleteMessageResponse>(entry.Entity);

        _logger.LogInformation("Message (Id: {MessageId}) was successfully removed.", model.MessageId);

        return response;
    }
}