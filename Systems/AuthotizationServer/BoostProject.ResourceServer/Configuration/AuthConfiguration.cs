using BoostProject.Common.Consts;
using BoostProject.Common.Enums;
using OpenIddict.Validation.AspNetCore;

namespace BoostProject.ResourceServer.Configuration;

public static class AuthConfiguration
{
    public static void AddAppAuth(this IServiceCollection services)
    {
        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        services.AddAuthorization(options =>
        {
            options.AddPolicy(CorsConsts.DefaultOriginName,
                authBuilder =>
                {
                    authBuilder.RequireRole(nameof(UserPermissions.User));
                });
        });
    }

    public static void UseAppAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
