using AspNet.Security.OAuth.Vkontakte;
using BoostProject.Common.Application_Urls;
using BoostProject.Settings.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace BoostProject.Common.Auth;

public static class IdentityAuthManager
{
    public static void ConfigureGoogleOptions(GoogleOptions options, IAppSettings settings)
    {
        options.ClientId = settings.Google.ClientId;
        options.ClientSecret = settings.Google.ClientSecret;
    }

    public static void ConfigureVkontakteOptions(VkontakteAuthenticationOptions options, IAppSettings settings)
    {
        options.ClientId = settings.Vkontakte.ClientId;
        options.ClientSecret = settings.Vkontakte.ClientSecret;
    }

    public static void ConfigureCookiesOptions(CookieAuthenticationOptions options, IAppSettings settings)
    {
        options.ClaimsIssuer = settings.Identity.Url;
        options.LoginPath = new Uri($"{settings.Identity.Url}/{AppUrls.AuthorizationServer.AuthenticatePage}", UriKind.Absolute).AbsoluteUri;
        
        options.ForwardDefault = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ForwardChallenge = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ForwardAuthenticate = CookieAuthenticationDefaults.AuthenticationScheme;
    }
}