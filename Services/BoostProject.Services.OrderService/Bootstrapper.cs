using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Services.OrdersService;

public static class Bootstrapper
{
    public static IServiceCollection AddOrdersService(this IServiceCollection services)
    {
        services.AddScoped<IOrdersService, OrdersService>();

        return services;
    }
}
