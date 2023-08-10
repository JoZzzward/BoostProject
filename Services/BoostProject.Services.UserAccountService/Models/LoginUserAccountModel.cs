using BoostProject.Errors;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BoostProject.Services.UserAccountService.Models;

public class LoginUserAccountModel
{
    public string Login { get; set; }
    public string Password { get; set; }
}

public partial class LoginUserAccountModelValidator : AbstractValidator<LoginUserAccountModel>
{
    public LoginUserAccountModelValidator()
    {
        RuleFor(x => x.Login)
            .MaximumLength(50).WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailMustBeLessThan50Symbols));

        RuleFor(x => x.Password)
            .Must(x => PasswordRegex().Matches(x).Count > 0)
            .WithMessage("Password must be 8 symbols or more")
            .Must(x => PasswordRegex().Matches(x).Count > 0)
            .WithMessage("Password must have minimum 1 lowercase letter");
    }

    [GeneratedRegex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$")]
    private static partial Regex PasswordRegex();
}