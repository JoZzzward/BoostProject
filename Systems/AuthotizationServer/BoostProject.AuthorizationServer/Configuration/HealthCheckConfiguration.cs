using BoostProject.AuthorizationServer.Configuration.HealthChecks;
using BoostProject.Common.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BoostProject.AuthorizationServer.Configuration;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<SelfHealthCheck>("BoostProject.AuthorizationServer");

        return services;
    }

    public static void UseAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");

        app.MapHealthChecks("/health/detail", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckHelper.WriteHealthCheckResponse,
            AllowCachingResponses = false,
        });
    }
}