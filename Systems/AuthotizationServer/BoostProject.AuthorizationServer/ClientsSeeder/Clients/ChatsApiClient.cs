using BoostProject.Common.Security;
using BoostProject.Settings.Interfaces;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace BoostProject.AuthorizationServer.ClientsSeeder.Clients;

public static class ChatsApiClient
{
    public static async Task AddClient(IOpenIddictApplicationManager manager, IAppSettings settings)
    {
        var client = await manager.FindByClientIdAsync(settings.IdentityClients.ChatsApiClientId);

        if (client is not null)
            await manager.DeleteAsync(client);

        await manager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = settings.IdentityClients.ChatsApiClientId,
            ClientSecret = settings.IdentityClients.ChatsApiClientSecret,
            ConsentType = ConsentTypes.Explicit,
            DisplayName = "Chats-Api-client",
            RedirectUris =
            {
                new Uri(settings.IdentityClients.ChatsApiRedirectUri)
            },
            PostLogoutRedirectUris =
            {
                new Uri(settings.IdentityClients.ChatsApiPostLogoutRedirectUri)
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

                $"{Permissions.Prefixes.Scope}{AppScopes.MessageAccess}",
                $"{Permissions.Prefixes.Scope}{AppScopes.MessageWrite}",

                $"{Permissions.Prefixes.Scope}{AppScopes.UserAccountAccess}",
                $"{Permissions.Prefixes.Scope}{ApiResources.BoostProjectChatsAPI}"
            }
        });
    }
}
