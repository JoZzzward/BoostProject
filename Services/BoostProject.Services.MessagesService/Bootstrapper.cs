using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Services.MessagesService;

public static class Bootstrapper
{
    public static IServiceCollection AddMessagesService(this IServiceCollection services)
    {
        services.AddScoped<IMessagesService, MessagesService>();

        return services;
    }
}