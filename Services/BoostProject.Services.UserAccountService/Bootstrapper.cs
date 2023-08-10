﻿using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Services.UserAccountService;

public static class Bootstrapper
{
    public static IServiceCollection AddUserAccountService(this IServiceCollection services)
    {
        services.AddScoped<IUserAccountService, UserAccountService>();

        return services;
    }
}
