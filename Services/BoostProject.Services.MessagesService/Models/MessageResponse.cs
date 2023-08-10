using AutoMapper;
using BoostProject.Data.Entities.Messages;

namespace BoostProject.Services.MessagesService.Models;

public class MessageResponse
{
    public string ReceiverName { get; set; }
    public string Content { get; set; }
}
public class MessageResponseProfile : Profile
{
    public MessageResponseProfile()
    {
        CreateMap<Message, MessageResponse>()
            .ForMember(
                dest => dest.ReceiverName,
                opt => opt.MapFrom(x => x.Receiver.FirstName)
            );
    }
}