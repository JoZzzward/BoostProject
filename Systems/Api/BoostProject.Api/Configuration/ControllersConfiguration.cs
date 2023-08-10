using BoostProject.Common.Consts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Api.Configuration;

/// <summary>
/// Controllers and Views Configuration
/// </summary>
public static class ControllersConfiguration
{
    /// <summary>
    /// Adds controller and views setup
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddAppControllers(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }

    /// <summary>
    /// Adds controller and views setup
    /// </summary>
    /// <param name="app"></param>
    public static void UseControllers(this WebApplication app)
    {
        app.MapControllers()
            .RequireCors(CorsConsts.DefaultOriginName);
    }

}
