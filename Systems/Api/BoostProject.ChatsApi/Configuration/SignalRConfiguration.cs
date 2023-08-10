using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.ChatsApi.Configuration;

public static class SignalRConfiguration
{

    public static IServiceCollection AddAppSignalR(this IServiceCollection services)
    {
        services.AddSignalR()
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });

        return services;
    }
}