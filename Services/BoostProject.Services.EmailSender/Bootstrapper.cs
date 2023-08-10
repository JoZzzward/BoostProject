using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Services.EmailSender;

public static class Bootstrapper
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services, IConfiguration configuration = null)
    {
        services.AddSingleton<IEmailSender, EmailSender>();
         
        return services;
    }
}
