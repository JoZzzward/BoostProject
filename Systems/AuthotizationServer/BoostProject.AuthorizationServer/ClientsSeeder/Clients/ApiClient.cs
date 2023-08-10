using BoostProject.Common.Security;
using BoostProject.Settings.Interfaces;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace BoostProject.AuthorizationServer.ClientsSeeder.Clients;

public static class ApiClient
{
    public static async Task AddClient(IOpenIddictApplicationManager manager, IAppSettings settings)
    {
        var client = await manager.FindByClientIdAsync(settings.IdentityClients.ApiClientId);

        if (client is not null)
            await manager.DeleteAsync(client);

        await manager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = settings.IdentityClients.ApiClientId,
            ClientSecret = settings.IdentityClients.ApiClientSecret,
            ConsentType = ConsentTypes.Explicit,
            DisplayName = "Api-client",
            RedirectUris =
            {
                new Uri(settings.IdentityClients.ApiRedirectUri)
            },
            PostLogoutRedirectUris =
            {
                new Uri(settings.IdentityClients.ApiPostLogoutRedirectUri)
            },
            Permissions =
            {
                Permissions.Endpoints.Authorization,
                Permissions.Endpoints.Token,
                Permissions.Endpoints.Logout,

                Permissions.GrantTypes.AuthorizationCode,
                Permissions.ResponseTypes.Code,

                Permissions.Scopes.Email,
                Permissions.Scopes.Profile,
                Permissions.Scopes.Roles,

                $"{Permissions.Prefixes.Scope}{AppScopes.GameAccountsWrite}",
                $"{Permissions.Prefixes.Scope}{AppScopes.GetUnverifiedGameAccounts}",
                $"{Permissions.Prefixes.Scope}{AppScopes.VerifyAccount}",

                $"{Permissions.Prefixes.Scope}{AppScopes.FeedbackWrite}",

                $"{Permissions.Prefixes.Scope}{AppScopes.MessageAccess}",
                $"{Permissions.Prefixes.Scope}{AppScopes.MessageWrite}",

                $"{Permissions.Prefixes.Scope}{AppScopes.OrdersAccess}",

                $"{Permissions.Prefixes.Scope}{AppScopes.UserAccountAccess}",
                $"{Permissions.Prefixes.Scope}{ApiResources.BoostProjectAPI}"
            }
        });
    }
}
