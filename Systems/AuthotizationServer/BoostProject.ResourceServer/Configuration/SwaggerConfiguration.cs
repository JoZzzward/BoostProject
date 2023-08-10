using BoostProject.Common.Application_Urls;
using BoostProject.Common.Security;
using BoostProject.Settings.Interfaces;
using Microsoft.OpenApi.Models;

namespace BoostProject.ResourceServer.Configuration;

public static class SwaggerConfiguration
{
    private static readonly string _appTitle = "BoostProject Resource Owner Api";

    public static void AddAppSwaggerUI(this IServiceCollection services, IAppSettings settings)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{settings.Identity.Url}/{AppUrls.AuthorizationServer.Authorization}"),
                        TokenUrl = new Uri($"{settings.Identity.Url}/{AppUrls.AuthorizationServer.Token}"),
                        Scopes = AppScopesManager.GenerateDictionary()
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void UseAppSwagger(this WebApplication app, IAppSettings settings)
    {
        app.UseSwagger();

        app.UseSwaggerUI(
            options =>
            {
                options.OAuthAppName(_appTitle);
                options.OAuthClientId(settings.IdentityClients.ResourceOwnerClientId);
                options.OAuthClientSecret(settings.IdentityClients.ResourceOwnerClientSecret);
        });
    }
}
