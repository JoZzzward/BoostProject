using BoostProject.ChatsApi.Configuration.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.ChatsApi.Configuration;

/// <summary>
/// HealthCheck Configuration
/// </summary>
public static class HealthCheckConfiguration
{
    /// <summary>
    /// Adds healthchecks settings
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<HealthCheck>("BoostProject.ChatsApi");

        return services;
    }

    /// <summary>
    /// Adds healthchecks endpoints 
    /// </summary>
    /// <param name="app"></param>
    public static void UseHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/healthy");
    }
}