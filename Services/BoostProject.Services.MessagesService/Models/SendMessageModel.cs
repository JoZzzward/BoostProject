using AutoMapper;
using BoostProject.Data.Entities.Messages;
using BoostProject.Errors;
using FluentValidation;

namespace BoostProject.Services.MessagesService.Models;

public class SendMessageModel
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string? Content { get; set; }
}
    
public class SendMessageModelProfile : Profile
{
    public SendMessageModelProfile()
    {
        CreateMap<SendMessageModel, Message>();
    }
}

public class SendMessageModelValidation : AbstractValidator<SendMessageModel>
{
    public SendMessageModelValidation()
    {
        RuleFor(x => x.SenderId)
            .NotEqual(Guid.Empty)
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.Message.UserIdIsIncorrect));
        
        RuleFor(x => x.ReceiverId)
            .NotEqual(Guid.Empty)
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.Message.UserIdIsIncorrect));

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.Message.MessageContentIsRequired))
            .NotNull()
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.Message.MessageContentIsRequired));
        ;
    }
}
