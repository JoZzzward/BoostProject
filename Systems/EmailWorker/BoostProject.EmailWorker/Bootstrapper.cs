using BoostProject.EmailWorker.EmailTask;
using BoostProject.Services.Actions;
using BoostProject.Services.EmailSender;
using BoostProject.Services.RabbitMqService;

namespace BoostProject.EmailWorker;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddRabbitMqService()
            .AddActionsService()
            .AddEmailSender()
            ;

        services.AddSingleton<ITaskEmailSender, TaskEmailSender>();

        return services;
    }
}
