namespace BoostProject.EmailWorker.Configuration;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<HealthCheck>("BoostProject.EmailWorker");
        
        return services;
    }

    public static void UseHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/healthy");
    }
}