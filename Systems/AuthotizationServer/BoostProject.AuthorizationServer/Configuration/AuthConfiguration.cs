using Microsoft.AspNetCore.Authentication.Cookies;

namespace BoostProject.AuthorizationServer.Configuration;

public static class AuthConfiguration
{
    public static IServiceCollection AddAppAuth(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                c => c.LoginPath = "/Authenticate"); 
        ;

        services.ConfigureApplicationCookie(c =>
        {
            c.ForwardDefault = CookieAuthenticationDefaults.AuthenticationScheme;
            c.LoginPath = "/Authenticate";
        });

        return services;
    }
}
