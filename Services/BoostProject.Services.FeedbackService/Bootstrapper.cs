using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Services.FeedbackService;

public static class Bootstrapper
{
    public static IServiceCollection AddFeedbackService(this IServiceCollection services)
    {
        services.AddScoped<IFeedbackService, FeedbackService>();

        return services;
    }
}
