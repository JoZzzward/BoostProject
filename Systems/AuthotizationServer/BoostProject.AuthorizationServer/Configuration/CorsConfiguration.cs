using BoostProject.Common.Application_Urls;
using BoostProject.Common.Consts;

namespace BoostProject.AuthorizationServer.Configuration;

public static class CorsConfiguration
{
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(
                    AppUrls.Api.MainUri,
                    AppUrls.ChatsApi.MainUri,
                    AppUrls.ResourceOwnerServer.MainUri)
                    .AllowAnyHeader();
            });

            builder.AddPolicy(CorsConsts.DefaultOriginName, policy =>
            {
                policy.WithOrigins(
                    AppUrls.Api.MainUri,
                    AppUrls.ChatsApi.MainUri,
                    AppUrls.ResourceOwnerServer.MainUri)
                                   .AllowAnyMethod()
                                   .AllowAnyHeader();
            });
        });

        return services;
    }

    public static void UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(CorsConsts.DefaultOriginName);
    }
}