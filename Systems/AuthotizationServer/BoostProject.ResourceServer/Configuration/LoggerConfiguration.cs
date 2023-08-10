using Serilog;

namespace BoostProject.ResourceServer.Configuration;

public static class LoggerConfiguration
{
    public static void AddLogger(this WebApplicationBuilder app)
    {
        app.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .Enrich.WithCorrelationId()
                .ReadFrom.Configuration(context.Configuration);
        });
    }

    public static IApplicationBuilder UseAppSerilog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();

        return app;
    }
}