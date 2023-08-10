using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Services.Actions;

public static class Bootstrapper
{
    public static IServiceCollection AddActionsService(this IServiceCollection services)
    {
        services.AddSingleton<IActionsService, ActionsService>();

        return services;
    }
}
