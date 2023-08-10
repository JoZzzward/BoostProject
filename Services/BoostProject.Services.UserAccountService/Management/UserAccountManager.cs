using BoostProject.Common.Exceptions;
using BoostProject.Common.Extensions;
using BoostProject.Data.Entities.AppUsers;
using BoostProject.Errors;
using Microsoft.AspNetCore.Identity;

namespace BoostProject.Services.UserAccountService.Management;

public class UserAccountManager
{
    private readonly UserManager<AppUser> _userManager;

    public UserAccountManager(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    protected async Task<AppUser> FindUserByEmail(string email)
    {
        var user = await _userManager!.FindByEmailAsync(email);

        ProcessException.ThrowIf(() => user == null, LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserNotFound));

        return user!;
    }

    /// <summary>
    /// Searching user by email than if user is not found searching by username if its specified.
    /// <para></para>
    /// Returns <see cref="Nullable"/> if user was not found
    /// </summary>
    /// <param name="email">User email</param>
    /// <param name="username">User username</param>
    /// <returns>Founded user</returns>
    protected async Task<AppUser?> IsUserExists(string email, string username = "")
    {
        var user = await _userManager.FindByEmailAsync(email.RemoveWhiteSpaces());

        user ??= await _userManager.FindByNameAsync(username);

        return user;
    }

    protected async Task<AppUser> FindUserByUserNameOrEmail(string login)
    {
        var user = await _userManager.FindByEmailAsync(login);

        user ??= await _userManager.FindByNameAsync(login);

        if (user is null)
            throw new ProcessException(LocalizedErrorsManager.GetMessage(ErrorLabels.UserAccount.UserNotFound));

        return user;
    }
}