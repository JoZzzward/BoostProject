using AutoMapper;
using BoostProject.Errors;
using BoostProject.Services.MessagesService.Models;
using FluentValidation;

namespace BoostProject.ChatsApi.Controllers.Messages.Models;

public class SendMessageRequest
{
    public Guid? ChatId { get; set; }

    public Guid? SenderId { get; set; }

    public Guid? ReceiverId { get; set; }

    public string Content { get; set; }
}

public class SendMessageRequestProfile : Profile
{
    public SendMessageRequestProfile()
    {
        CreateMap<SendMessageRequest, SendMessageModel>();
    }
}

public class SendMessageRequestValidation : AbstractValidator<SendMessageRequest>
{
    public SendMessageRequestValidation()
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
