using AutoMapper;
using BoostProject.Common.Exceptions;
using BoostProject.Common.Validation;
using BoostProject.Data.Context;
using BoostProject.Data.Entities.Feedbacks;
using BoostProject.Errors;
using BoostProject.Services.FeedbackService.Models;
using BoostProject.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoostProject.Services.FeedbackService;

public class FeedbackService : ServiceManager, IFeedbackService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly ILogger<FeedbackService> _logger;
    private readonly IMapper _mapper;
    private readonly IModelValidator<CreateFeedbackModel> _createFeedbackModelValidation;

    public FeedbackService(
        IDbContextFactory<AppDbContext> dbContext,
        ILogger<FeedbackService> logger,
        IMapper mapper,
        IModelValidator<CreateFeedbackModel> createFeedbackModelValidation)
        : base(dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _createFeedbackModelValidation = createFeedbackModelValidation;
    }

    public async Task<IEnumerable<FeedbackResponse>> GetAllFeedbacks()
    {
        using var context = await _dbContext.CreateDbContextAsync();

        _logger.LogInformation("Returning feedbacks from database..");

        var data = await context
            .Feedbacks
            .ToListAsync() ?? new List<Feedback>();

        var response = data.Select(_mapper.Map<FeedbackResponse>);

        _logger.LogInformation("All feedbacks was returned successfully");

        return response;
    }
     
    public async Task<CreateFeedbackResponse> CreateFeedback(CreateFeedbackModel model)
    {
        ValidateCreateFeedback(model);

        using var context = await _dbContext.CreateDbContextAsync();

        var userDontHaveDoneOrders = context.Orders
            .Any(x => x.CustomerId == model.UserId && 
                 x.Status == Common.Enums.OrderStatus.Done);

        ProcessException.ThrowIf(() => userDontHaveDoneOrders, LocalizedErrorsManager.GetMessage(ErrorLabels.Feedback.UserNotAllowToCreateTheFeedback));

        var data = _mapper.Map<Feedback>(model);

        _logger.LogInformation("Saving feedbacks in database");

        var entry = await context.AddAsync(data);
        await context.SaveChangesAsync();

        _logger.LogInformation("Feedback was saved successfully");

        return _mapper.Map<CreateFeedbackResponse>(entry.Entity);
    }

    private void ValidateCreateFeedback(CreateFeedbackModel model)
    {
        _logger.LogInformation("Validating model on feedback creation");

        _createFeedbackModelValidation.CheckValidation(model);

        _logger.LogInformation("User verification");

        _ = IsUsersExists(model.UserId);
        
        _logger.LogInformation("Validation is confirmed.");
    }
}