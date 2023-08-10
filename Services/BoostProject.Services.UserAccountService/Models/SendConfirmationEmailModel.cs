using AutoMapper;
using BoostProject.Errors;
using FluentValidation;

namespace BoostProject.Services.UserAccountService.Models;

public class SendConfirmationEmailModel
{
    public string Email { get; set; }
}

public class SendConfirmationEmailModelValidator : AbstractValidator<SendConfirmationEmailModel>
{
    public SendConfirmationEmailModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailIncorrect))
            .MaximumLength(50).WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailMustBeLessThan50Symbols));
    }
}

public class SendConfirmationEmailModelProfile : Profile
{
    public SendConfirmationEmailModelProfile()
    {
        CreateMap<SendConfirmationEmailModel, SendConfirmationEmailResponse>();
    }
}
