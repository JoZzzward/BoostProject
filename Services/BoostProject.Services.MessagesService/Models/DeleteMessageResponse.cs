using AutoMapper;
using BoostProject.Data.Entities.Messages;

namespace BoostProject.Services.MessagesService.Models;

public class DeleteMessageResponse
{
    public Guid? MessageId { get; set; }
}

public class DeleteMessageResponseProfile : Profile
{
    public DeleteMessageResponseProfile()
    {
        CreateMap<Message, DeleteMessageResponse>();
    }
}