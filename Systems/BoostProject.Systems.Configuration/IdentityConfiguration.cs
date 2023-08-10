using BoostProject.Common.Enums;
using BoostProject.Data.Context;
using BoostProject.Data.Entities.AppUsers;
using BoostProject.Settings.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Validation;
using System.Diagnostics;

namespace BoostProject.Systems.Configuration;

public static class IdentityConfiguration
{
    public static void AddAppIdentity(this IServiceCollection services, IAppSettings settings, string audienceName)
    {
        IdentityModelEventSource.ShowPII = true;

        services
            .AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddUserManager<UserManager<AppUser>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddDefaultTokenProviders()
            ;

        services.AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer(settings.Identity.Url);
                options.AddAudiences(audienceName);

                options.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String(settings.Identity.SigningKey)));

                options.UseSystemNetHttp();

                options.UseAspNetCore();

                options.AddEventHandler<OpenIddictValidationEvents.ProcessChallengeContext>(builder =>
                {
                    builder.SetOrder(int.MinValue);

                    builder.UseInlineHandler(context =>
                    {
                        var trace = new StackTrace().ToString();

                        return default;
                    });
                });
            });
        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
            options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
            options.ClaimsIdentity.EmailClaimType = OpenIddictConstants.Claims.Email;
            options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
        });
        _ = CreateRoles(services.BuildServiceProvider());
    }

    private static async Task CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
        var roleNames = Enum.GetNames(typeof(UserPermissions));

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new AppRole(roleName));
            }
        }
    }
}
