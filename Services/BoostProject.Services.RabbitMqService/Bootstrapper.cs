using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Services.RabbitMqService;

public static class Bootstrapper
{
    public static IServiceCollection AddRabbitMqService(this IServiceCollection services, IConfiguration? configuration = null)
    {
        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        return services;
    }
}