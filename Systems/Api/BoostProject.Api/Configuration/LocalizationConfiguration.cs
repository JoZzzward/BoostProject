using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace BoostProject.Api.Configuration;

public static class LocalizationConfiguration
{
    public static IServiceCollection AddAppLocalization(this IServiceCollection services)
    {
        services.AddLocalization();

        return services;
    }

    public static void UseAppLocalization(this WebApplication app)
    {
        var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };

        var ci = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(ci),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });
    }

}