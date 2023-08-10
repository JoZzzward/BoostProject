using BoostProject.Data.Context.Factories;
using BoostProject.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Data.Context;

public static class Bootstrapper
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IAppSettings settings)
    {
        var options = DbContextOptionsFactory.Configure(settings.Db);

        services.AddDbContextFactory<AppDbContext>(options);

        return services;
    }
}