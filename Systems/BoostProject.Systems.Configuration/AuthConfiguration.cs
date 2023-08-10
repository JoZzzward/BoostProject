using BoostProject.Common.Auth;
using BoostProject.Common.Consts;
using BoostProject.Common.Enums;
using BoostProject.Common.Security;
using BoostProject.Settings.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace BoostProject.Systems.Configuration;

/// <summary>
/// Authentication and Authorization configuration
/// </summary>
public static class AuthConfiguration
{
    /// <summary>
    /// Adds Authentication and Authorization to services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="settings"></param>
    public static IServiceCollection AddAppAuth(this IServiceCollection services, IAppSettings settings)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        })/* TODO: Enable Google and Vkontakte authentication
            .AddGoogle(options =>
            {
                IdentityAuthManager.ConfigureGoogleOptions(options, settings);
            })
            .AddVkontakte(options =>
            {
                IdentityAuthManager.ConfigureVkontakteOptions(options, settings);
            })*/
            .AddCookie(options =>
            {
                IdentityAuthManager.ConfigureCookiesOptions(options, settings);
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(CorsConsts.DefaultOriginName,
                policy =>
                {
                    policy.RequireRole(nameof(UserPermissions.User));
                });

            AppScopesManager.ImplementAllByAction(scope =>
                {
                    options.AddPolicy(scope, policy => { policy.RequireClaim("scope", scope); policy.RequireRole(nameof(UserPermissions.User)); });
                });
        });

        return services;
    }

    /// <summary>
    /// Adds Authentication and Authorization to application
    /// </summary>
    public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}
