using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Systems.Configuration;

/// <summary>
/// AutoMapper Configuration
/// </summary>
public static class AutoMapperConfiguration
{
    /// <summary>
    /// Adds AutoMapper to domain assemblies which names starts with "BoostProject."
    /// </summary>
    public static IServiceCollection AddAppAutoMapper(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("boostproject."));

        services.AddAutoMapper(assemblies);

        return services;
    }
}
