using BoostProject.Errors;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BoostProject.Services.UserAccountService.Models;

public class ChangePasswordModel
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string CurrentPassword { get; set; }
}

public partial class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailIncorrect))
            .MaximumLength(50).WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailMustBeLessThan50Symbols));

        RuleFor(x => x.NewPassword)
            .Must(x => PasswordRegex().Matches(x).Count > 0)
            .WithMessage("Password must be 8 symbols or more")
            .Must(x => PasswordRegex().Matches(x).Count > 0)
            .WithMessage("Password must have minimum 1 lowercase letter");
    }

    [GeneratedRegex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$")]
    private static partial Regex PasswordRegex();
}