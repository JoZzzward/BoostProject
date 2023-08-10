using Microsoft.Extensions.DependencyInjection;
using BoostProject.Settings.Interfaces;
using BoostProject.Settings.Settings;
using BoostProject.Settings.Source;

namespace BoostProject.Settings;

public static class Bootstrapper
{
    public static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddSingleton<ISettingSource, SettingSource>();
        services.AddSingleton(typeof(IAppSettings), typeof(AppSettings));
        services.AddSingleton<IEmailSettings, EmailSettings>();
        services.AddSingleton<IIdentitySettings, IdentitySettings>();
        services.AddSingleton<IIdentityClientsSettings, IdentityClientsSettings>();
        services.AddSingleton<IRedisSettings, RedisSettings>();
        services.AddSingleton<IRabbitMqSettings, RabbitMqSettings>();
        services.AddSingleton<IDbSettings, DbSettings>();

        return services;
    }
}