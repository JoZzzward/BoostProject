using AutoMapper;
using BoostProject.Data.Entities.Messages;

namespace BoostProject.Services.MessagesService.Models;

public class SendMessageResponse
{
    public Guid? MessageId { get; set; }
}

public class SendMessageResponseProfile : Profile
{
    public SendMessageResponseProfile()
    {
        CreateMap<Message, SendMessageResponse>()
            .ForMember(
                dest => dest.MessageId,
                opt => opt.MapFrom(x => x.Id)
            );
    }
}