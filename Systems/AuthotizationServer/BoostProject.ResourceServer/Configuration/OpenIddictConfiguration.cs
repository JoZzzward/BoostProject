using BoostProject.Common.Security;
using BoostProject.Settings.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Quartz;

namespace BoostProject.ResourceServer.Configuration;

public static class OpenIddictConfiguration
{
    public static void AddAppOpenIddict(this IServiceCollection services, IAppSettings settings)
    {
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer(settings.Identity.Url);
                options.AddAudiences(ApiResources.BoostProjectResourceOwner);

                options.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String(settings.Identity.SigningKey)));

                options.UseSystemNetHttp();
                
                options.UseAspNetCore();
            });
    }
}
