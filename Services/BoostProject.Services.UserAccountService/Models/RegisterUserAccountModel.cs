using AutoMapper;
using FluentValidation;
using System.Text.RegularExpressions;
using BoostProject.Data.Entities.AppUsers;
using BoostProject.Errors;

namespace BoostProject.Services.UserAccountService.Models;

public class RegisterUserAccountModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public partial class RegisterUserAccountModelValidator : AbstractValidator<RegisterUserAccountModel>
{
    public RegisterUserAccountModelValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(30).WithMessage("Username length must be less than 30")
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailIncorrect))
            .MaximumLength(50).WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailMustBeLessThan50Symbols));

        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmPassword).WithMessage("Password and ConfirmPassword must be equals")
            .Must(x => PasswordRegex().Matches(x).Count > 0)
            .WithMessage("Password must be 8 symbols or more")
            .Must(x => PasswordRegex().Matches(x).Count > 0)
            .WithMessage("Password must have minimum 1 lowercase letter");
    }

    [GeneratedRegex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$")]
    private static partial Regex PasswordRegex();
}

public class RegisterUserAccountModelProfile : Profile
{
    public RegisterUserAccountModelProfile()
    {
        CreateMap<RegisterUserAccountModel, AppUser>()
            .ForSourceMember(dest => dest.Password, opt => opt.DoNotValidate())
            .ForSourceMember(dest => dest.ConfirmPassword, opt => opt.DoNotValidate());
    }
}
