using AutoMapper;
using BoostProject.Errors;
using FluentValidation;

namespace BoostProject.Services.UserAccountService.Models;

public class ConfirmationEmailModel
{
    public string Email { get; set; }
    public string Token { get; set; }
}

public class ConfirmationEmailModelValidator : AbstractValidator<ConfirmationEmailModel>
{
    public ConfirmationEmailModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailIncorrect))
            .MaximumLength(50).WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailMustBeLessThan50Symbols));

        RuleFor(x => x.Token)
            .Length(260, 270).WithMessage("Token must have correct length");
    }
}

public class ConfirmationEmailModelProfile : Profile
{
    public ConfirmationEmailModelProfile()
    {
        CreateMap<ConfirmationEmailModel, ConfirmationEmailResponse>();
    }
}