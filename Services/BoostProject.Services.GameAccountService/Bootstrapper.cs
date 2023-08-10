using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Services.GameAccountService;

public static class Bootstrapper
{
    public static IServiceCollection AddGameAccountService(this IServiceCollection services)
    {
        services.AddScoped<IGameAccountService, GameAccountService>();

        return services;
    }
}
