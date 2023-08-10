using BoostProject.Services.MessagesService.Models;

namespace BoostProject.Services.MessagesService;

public interface IMessagesService
{
    Task<IEnumerable<MessageResponse>> GetMessagesBySenderId(Guid senderId);
    Task<SendMessageResponse> SendMessage(SendMessageModel model);
    Task<DeleteMessageResponse> DeleteMessage(DeleteMessageModel model);
}