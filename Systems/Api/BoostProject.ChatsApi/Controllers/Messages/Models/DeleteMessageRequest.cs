using AutoMapper;
using BoostProject.Services.MessagesService.Models;

namespace BoostProject.ChatsApi.Controllers.Messages.Models;

public class DeleteMessageRequest
{
    public Guid SenderId { get; set; }
    public Guid MessageId { get; set; }
}

public class DeleteMessageRequestProfile : Profile
{
    public DeleteMessageRequestProfile()
    {
        CreateMap<DeleteMessageRequest, DeleteMessageModel>();
    }
}
