using BoostProject.Common.Application_Urls;
using BoostProject.Common.Security;
using BoostProject.Settings.Interfaces;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace BoostProject.ChatsApi.Configuration;

public static class SwaggerConfiguration
{
    private static readonly string _appTitle = "BoostProject Chats Api";

    /// <summary>
    /// Add OpenAPI for API
    /// </summary>  
    /// <param name="services">Services collection</param>
    /// <param name="settings">IdentityServer settings</param>
    public static IServiceCollection AddAppSwagger(this IServiceCollection services, IAppSettings settings)
    {
        services
            .AddOptions<SwaggerGenOptions>()
            .Configure<IApiVersionDescriptionProvider>((options, provider) =>
            {
                foreach (var avd in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(avd.GroupName, new OpenApiInfo
                    {
                        Version = avd.GroupName,
                        Title = $"{_appTitle}"
                    });
                }
            });

        services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();

            options.UseInlineDefinitionsForEnums();

            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            options.DescribeAllParametersInCamelCase();

            var xmlFile = "api.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

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

            options.UseOneOfForPolymorphism();
            options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

            options.ExampleFilters();
        });

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }


    /// <summary>
    /// Start OpenAPI UI
    /// </summary>
    /// <param name="app">Web application</param>
    public static void UseAppSwagger(this WebApplication app, IAppSettings settings)
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger(options =>
        {
            options.RouteTemplate = "{documentname}/api.yaml";
        });

        app.UseSwaggerUI(
            options =>
            {
                options.RoutePrefix = "";
                provider.ApiVersionDescriptions.ToList().ForEach(
                    description => options.SwaggerEndpoint($"/{description.GroupName}/api.yaml", description.GroupName.ToUpperInvariant())
                );

                options.DocExpansion(DocExpansion.List);
                options.DefaultModelsExpandDepth(-1);
                options.OAuthAppName(_appTitle);
                options.OAuthClientId(settings.IdentityClients.ChatsApiClientId);
                options.OAuthClientSecret(settings.IdentityClients.ChatsApiClientSecret);
            }
        );
    }
}
