using BoostProject.ChatsApi.Models;
using BoostProject.Common.Security;
using BoostProject.Common.Extensions;
using BoostProject.Services.MessagesService;
using BoostProject.Services.MessagesService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using BoostProject.Errors;
using Microsoft.IdentityModel.Tokens;

namespace BoostProject.ChatsApi.Hubs;

public class ChatHub : Hub
{
    private readonly IMessagesService _messagesService;

    public ChatHub(IMessagesService messagesService)
    {
        _messagesService = messagesService;
    }

    [Authorize(Policy = AppScopes.MessageWrite)]
    public async Task<MessageSendResult> SendMessageToUser(SendMessageModel model)
    {
        var message = model.Content;

        if (message.IsNullOrEmpty())
            return new MessageSendResult(LocalizedErrorsManager.GetMessage(ErrorLabels.Message.MessageContentIsRequired));

        var response = await _messagesService.SendMessage(model);

        if (response.MessageId.IsNullOrDefault())
            return new MessageSendResult($"Error while saving message from sender (Id: {model.SenderId}) to database.");

        await Clients.User(model.ReceiverId.ToString()).SendAsync(message!);

        return new MessageSendResult();
    }
}
