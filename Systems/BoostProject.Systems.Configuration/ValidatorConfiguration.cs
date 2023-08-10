using BoostProject.Common.Helpers;
using BoostProject.Common.Validation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Systems.Configuration;

/// <summary>
/// Validator Configuration
/// </summary>
public static class ValidatorConfiguration
{
    /// <summary>
    /// Adds Fluent Validation to project services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        ValidatorsRegisterHelper.Register(services);

        services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return services;
    }
}
