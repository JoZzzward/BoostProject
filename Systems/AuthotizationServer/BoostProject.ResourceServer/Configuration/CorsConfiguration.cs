using BoostProject.Common.Application_Urls;
using BoostProject.Common.Consts;

namespace BoostProject.ResourceServer.Configuration;

/// <summary>
/// CORS configuration
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Adds CORS to application 
    /// </summary>
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddPolicy(CorsConsts.DefaultOriginName, policy =>
            {
                policy.WithOrigins(AppUrls.AuthorizationServer.MainUri)
                                   .AllowAnyMethod()
                                   .AllowAnyHeader();
            });
        });

        return services;
    }

    /// <summary>
    /// Adds CORS using to application
    /// </summary>
    /// <param name="app">Application</param>
    public static void UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(CorsConsts.DefaultOriginName);
    }
}