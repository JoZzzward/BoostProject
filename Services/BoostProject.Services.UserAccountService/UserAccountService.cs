using AutoMapper;
using BoostProject.Common.Exceptions;
using BoostProject.Common.Extensions;
using BoostProject.Common.Validation;
using BoostProject.Services.EmailSender;
using BoostProject.Services.UserAccountService.Helpers;
using BoostProject.Services.UserAccountService.Models;
using BoostProject.Data.Entities.AppUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using BoostProject.Errors;
using BoostProject.Services.UserAccountService.Management;
using BoostProject.Common.Enums;

namespace BoostProject.Services.UserAccountService;

public class UserAccountService : UserAccountManager, IUserAccountService
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<UserAccountService> _logger;
    private readonly IModelValidator<RegisterUserAccountModel> _registerModelValidator;
    private readonly IModelValidator<LoginUserAccountModel> _loginModelValidator;
    private readonly IModelValidator<ConfirmationEmailModel> _confirmationEmailModelValidator;
    private readonly IModelValidator<SendPasswordRecoveryModel> _sendPasswordRecoveryModelValidator;
    private readonly IModelValidator<PasswordRecoveryModel> _passwordRecoveryModelValidator;
    private readonly IModelValidator<ChangePasswordModel> _changePasswordModelValidator;
    private readonly IModelValidator<SendConfirmationEmailModel> _sendConfirmationEmailModelValidator;

    public UserAccountService(
        IMapper mapper,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<AppRole> roleManager,
        IEmailSender emailSender,
        ILogger<UserAccountService> logger,
        IModelValidator<RegisterUserAccountModel> registerModelValidator,
        IModelValidator<SendConfirmationEmailModel> sendConfirmationEmailModelValidator,
        IModelValidator<LoginUserAccountModel> loginModelValidator,
        IModelValidator<ConfirmationEmailModel> confirmationEmailModelValidator,
        IModelValidator<SendPasswordRecoveryModel> sendPasswordRecoveryModelValidator,
        IModelValidator<PasswordRecoveryModel> passwordRecoveryModelValidator,
        IModelValidator<ChangePasswordModel> changePasswordModelValidator
        ) : base(userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _emailSender = emailSender;
        _logger = logger;
        _registerModelValidator = registerModelValidator;
        _sendConfirmationEmailModelValidator = sendConfirmationEmailModelValidator;
        _loginModelValidator = loginModelValidator;
        _confirmationEmailModelValidator = confirmationEmailModelValidator;
        _sendPasswordRecoveryModelValidator = sendPasswordRecoveryModelValidator;
        _passwordRecoveryModelValidator = passwordRecoveryModelValidator;
        _changePasswordModelValidator = changePasswordModelValidator;
    }

    public async Task<RegisterUserAccountResponse?> RegisterUser(RegisterUserAccountModel model)
    {
        await _registerModelValidator.CheckValidation(model);

        var user = await IsUserExists(model.Email, model.UserName);

        if (user != null)
            throw new ProcessException(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserWithThisLoginExists));

        user = _mapper.Map<AppUser>(model);

        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (!result.Succeeded)
            throw new ProcessException(result.Errors.Select(s => s.Description).First());

        await SendEmailConfirmationMail(user);

        await _userManager.AddToRoleAsync(user, nameof(UserPermissions.User));

        var response = _mapper.Map<RegisterUserAccountResponse>(user);

        _logger.LogInformation("--> User (Email: {UserEmail}) successfully registered", response.Email);

        return response;
    }

    public async Task<LoginUserAccountResponse?> LoginUser(LoginUserAccountModel model)
    {
        await _loginModelValidator.CheckValidation(model);

        var user = await FindUserByUserNameOrEmail(model.Login);
            
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

        ProcessException.ThrowIf(() => !result.Succeeded, $"User (Email: {model.Login}) can not signed in.");

        var response = _mapper.Map<LoginUserAccountResponse>(user);

        _logger.LogInformation("--> User (Email: {UserEmail}) successfully logged in.", response.Email);

        return response;
    }

    public async Task<SendConfirmationEmailResponse?> SendConfirmEmail(SendConfirmationEmailModel model)
    {
        await _sendConfirmationEmailModelValidator.CheckValidation(model);

        var user = await FindUserByEmail(model.Email);

        var resultSucceeded = await SendEmailConfirmationMail(user);

        if (!resultSucceeded) return default;

        return _mapper.Map<SendConfirmationEmailResponse>(model);
    }

    public async Task<ConfirmationEmailResponse?> ConfirmEmail(ConfirmationEmailModel model)
    {
        await _confirmationEmailModelValidator.CheckValidation(model);

        var user = await FindUserByEmail(model.Email);

        var result = await _userManager.ConfirmEmailAsync(user, model.Token);

        if(!result.Succeeded)
        {
            var error = result.Errors.Select(s => s.Description).FirstOrDefault() ?? "ErrorNotFound";
            _logger.LogError("User (Email: {UserEmail}) can not confirm his email. Error: {Error}", model.Email, error);
            throw new ProcessException(error);
        }

        _logger.LogInformation("--> User (Email: {UserEmail}) successfully confirmed his email.", model.Email);

        return _mapper.Map<ConfirmationEmailResponse>(model);
    }

    public async Task<PasswordRecoveryResponse?> SendRecoveryPasswordEmail(SendPasswordRecoveryModel model)
    {
        await _sendPasswordRecoveryModelValidator.CheckValidation(model);

        var user = await FindUserByEmail(model.Email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var content = ContentReader.ReadFromFile("passwordRecovery.html", user.Email, token);

        // Sending mail to user email for password recovery
        await _emailSender.SendEmail(new EmailModel
        {
            EmailTo = user.Email!,
            Subject = "Password recovery message",
            Message = content
        });

        _logger.LogInformation("--> Password message successfully was sent to User (Email: {UserEmail})", user.Email);

        return _mapper.Map<PasswordRecoveryResponse>(user);
    }

    public async Task<PasswordRecoveryResponse?> RecoverPassword(PasswordRecoveryModel model)
    {
        await _passwordRecoveryModelValidator.CheckValidation(model);

        var user = await FindUserByEmail(model.Email);

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

        if (!result.Succeeded)
        {
            var error = result.Errors.Select(s => s.Description).FirstOrDefault();

            _logger.LogError("--> User (Email: {model.Email}) can not recover his password. Error: {Error}", model.Email, error);
            throw new ProcessException(error);
        }

        var response = _mapper.Map<PasswordRecoveryResponse>(user);

        _logger.LogInformation("--> Password of User(Email: {UserEmail}) was successfully recovered.", response.Email);

        return response;
    }

    public async Task<ChangePasswordResponse?> ChangePassword(ChangePasswordModel model)
    {
        await _changePasswordModelValidator.CheckValidation(model);

        var user = await FindUserByEmail(model.Email);

        // Compares old password with current
        var passwordVerifiedStatus =
            new PasswordHasher<AppUser>().VerifyHashedPassword(user, user.PasswordHash!, model.CurrentPassword);

        ProcessException.ThrowIf(
            () => passwordVerifiedStatus == PasswordVerificationResult.Failed,
            LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserWhileResetPassword));

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Changes password
        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

        ProcessException.ThrowIf(
            () => !result.Succeeded, 
            $"User (Email: {model.Email}) can not change his password. Error: {result.Errors.Select(s => s.Description).FirstOrDefault()}");

        var response = _mapper.Map<ChangePasswordResponse>(user);

        _logger.LogInformation("--> Password of User (Email: {UserEmail}) was successfully changed.", response.Email);

        return response;
    }

    private async Task<bool> SendEmailConfirmationMail(AppUser user)
    {
        ProcessException.ThrowIf(
            () => user.Email == null,
            LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserEmailIncorrect));

        _logger.LogInformation("--> Trying to send message with email confirmation link to user (Email: {UserEmail})", user.Email);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var content = ContentReader.ReadFromFile("emailConfirmation.html", user.Email!, token);

        // Setting first letter to uppercase
        var username = $"{char.ToUpper(user.UserName![0])}{user.UserName[1..]}";

        // Sending email confirmation mail for current user
        _emailSender?.SendEmail(new EmailModel
        {
            EmailTo = user.Email!,
            Subject = $"Hello, dear {username}!",
            Message = content
        });

        _logger.LogInformation("--> Email confirmation message was sent to user (Email: {UserEmail})", user.Email);

        return true;
    }
}