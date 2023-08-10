using BoostProject.Services.EmailSender;
using BoostProject.Services.MessagesService;
using BoostProject.Services.UserAccountService;
using BoostProject.Services.RabbitMqService;
using BoostProject.Services.Actions;
using BoostProject.Services.FeedbackService;
using BoostProject.Services.GameAccountService;
using BoostProject.Services.OrdersService;
using BoostProject.Errors;

namespace BoostProject.Api;

/// <summary>
/// Loads all services
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Registers all services to application
    /// </summary>
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services.AddMessagesService()
            .AddFeedbackService()
            .AddGameAccountService()
            .AddOrdersService()
            .AddUserAccountService()
            .AddEmailSender()
            .AddRabbitMqService()
            .AddActionsService()
            ;

        return services;
    }
}
