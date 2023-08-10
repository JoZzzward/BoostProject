namespace BoostProject.Errors.ErrorTypes;

public class UserAccountErrors
{
    public readonly string UserYouBanned = "UserYouBanned";
    public readonly string UserUsernameMustBeLessThan30Symbols = "UserUsernameMustBeLessThan30Symbols";
    public readonly string UserIncorrectEmailOrPassword = "UserIncorrectEmailOrPassword";
    public readonly string UserWithThisLoginExists = "UserWithThisLoginExists";
    public readonly string UserUpdateEntity = "UserUpdateEntity";
    public readonly string UserSaveEntity = "UserSaveEntity";
    public readonly string UserWhileResetPassword = "UserWhileResetPassword";
    public readonly string UserIsBanned = "UserIsBanned";
    public readonly string UserIsNotAdmin = "UserIsNotAdmin";
    public readonly string UserNotFound = "UserNotFound";
    public readonly string UserOnlyAdminGetAccess = "UserOnlyAdminGetAccess";
    public readonly string UserEmailIncorrect = "UserEmailIncorrect";
    public readonly string UserEmailMustBeLessThan50Symbols = "UserEmailMustBeLessThan50Symbols";
}