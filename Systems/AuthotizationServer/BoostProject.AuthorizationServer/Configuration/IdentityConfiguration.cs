using BoostProject.AuthorizationServer.Configuration.Factories;
using BoostProject.AuthorizationServer.Configuration.IdentitySettings;
using BoostProject.Common.Application_Urls;
using BoostProject.Common.Security;
using BoostProject.Data.Context;
using BoostProject.Data.Entities.AppUsers;
using BoostProject.Settings.Interfaces;
using Microsoft.AspNetCore.Identity;
using Quartz;

namespace BoostProject.AuthorizationServer.Configuration;

public static class IdentityConfiguration
{
    public static IServiceCollection AddAppIdentity(this IServiceCollection services, IAppSettings settings)
    {
        services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddClaimsPrincipalFactory<ClaimsFactory<AppUser>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddUserManager<UserManager<AppUser>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddDefaultTokenProviders()
            ;

        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<AppDbContext>()
                    .ReplaceDefaultEntities<Guid>();
                options.UseQuartz();
            })
            .AddServer(options =>
            {
                options.SetAuthorizationEndpointUris(AppUrls.AuthorizationServer.Authorization)
                    .SetTokenEndpointUris(AppUrls.AuthorizationServer.Token)
                    .SetLogoutEndpointUris(AppUrls.AuthorizationServer.Logout)
                    .SetUserinfoEndpointUris(AppUrls.AuthorizationServer.UserInfo);
                // TODO: Add UserInfo endpoint/controller

                options.AllowAuthorizationCodeFlow();

                options.SetAccessTokenLifetime(TimeSpan.FromDays(settings.Identity.AccessTokenLifetime))
                    .SetRefreshTokenLifetime(TimeSpan.FromDays(settings.Identity.RefreshTokenLifetime));

                options.EnableEncryptions(settings);

                options.RegisterScopesAndClaims();

                options.UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough()
                    .EnableStatusCodePagesIntegration()
                ;
            })
            .AddValidation(options =>
            {
                var audiences = ApiResourcesManager.GetAllResources();

                options.AddAudiences(audiences.ToArray());
            });

        return services;
    }
}