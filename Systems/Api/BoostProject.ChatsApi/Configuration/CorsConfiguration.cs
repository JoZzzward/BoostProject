using BoostProject.Common.Application_Urls;
using BoostProject.Common.Consts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.ChatsApi.Configuration;

/// <summary>
/// CORS configuration
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Adds CORS to application 
    /// </summary>
    /// <param name="services">Services collection</param>
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddPolicy(CorsConsts.DefaultOriginName, policy =>
            {
                policy.WithOrigins(AppUrls.Web.MainUri)
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