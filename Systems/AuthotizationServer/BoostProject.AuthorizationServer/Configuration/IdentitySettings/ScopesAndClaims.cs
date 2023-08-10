using BoostProject.Common.Security;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace BoostProject.AuthorizationServer.Configuration.IdentitySettings;

public static class ScopesAndClaims
{
    public static void RegisterScopesAndClaims(this OpenIddictServerBuilder options)
    {
        var scopes = AppScopesManager.GetAllScopes().ToList();

        scopes.Add(OpenIddictConstants.Scopes.Email);
        scopes.Add(OpenIddictConstants.Scopes.Profile);
        scopes.Add(OpenIddictConstants.Scopes.Roles);
        scopes.Add(OpenIddictConstants.Scopes.OfflineAccess);

        options.RegisterScopes(scopes.ToArray());

        options.RegisterClaims(
            OpenIddictConstants.Claims.Profile,
            OpenIddictConstants.Claims.Role,
            OpenIddictConstants.Claims.Email
        );
    }
}
