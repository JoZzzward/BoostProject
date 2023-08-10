using OpenIddict.Abstractions;

namespace BoostProject.AuthorizationServer.Configuration.IdentitySettings;

public static class AppClients
{
    public static async Task RegisterClients(IServiceProvider serviceProvider)
    {
        var applicationManager = serviceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await applicationManager.FindByClientIdAsync("frontend") is null)
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "frontend",
                DisplayName = "Frontend",
                ClientSecret = "secret",
                Permissions =
                    {
                        OpenIddictConstants.Parameters.AccessToken,
                        OpenIddictConstants.Parameters.Scope,
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.TokenTypeHints.AccessToken,
                        OpenIddictConstants.TokenTypeHints.RefreshToken,
                        OpenIddictConstants.TokenTypeHints.AuthorizationCode,
                        OpenIddictConstants.TokenTypeHints.UserinfoToken,
                        OpenIddictConstants.Scopes.OfflineAccess,
                        OpenIddictConstants.Scopes.Roles,
                        OpenIddictConstants.Scopes.Email,
                        OpenIddictConstants.Scopes.Profile
                    },

            });
        if (await applicationManager.FindByClientIdAsync("postman") is null)
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "postman",
                DisplayName = "Postman",
                RedirectUris = { new Uri("https://www.getpostman.com/oauth2/callback") },
                Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

                        OpenIddictConstants.Permissions.ResponseTypes.CodeIdToken
                    }
            });
    }
}
