using BoostProject.Common.Consts;
using BoostProject.Services.EmailSender;
using BoostProject.Services.RabbitMqService;

namespace BoostProject.EmailWorker.EmailTask;

public class TaskEmailSender : ITaskEmailSender
{
    private readonly ILogger<TaskEmailSender> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IRabbitMqService _rabbitMqService;

    public TaskEmailSender(
        ILogger<TaskEmailSender> logger,
        IServiceProvider serviceProvider,
        IRabbitMqService rabbitMqService
        )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _rabbitMqService = rabbitMqService;
    }

    public void Start()
    {
        _rabbitMqService.Subscribe<EmailModel>(ActionConsts.SEND_EMAIL_NOTIFICATION, async data
            => await Execute<IEmailSender>(async service =>
            {
                if (data is null)
                    return;

                _logger.LogInformation("--> RABBITMQ: TRYING TO SEND NOTIFICATION TO: {EmailTo}", data.EmailTo);
                    
                await service.SendEmail(data);

                _logger.LogInformation("--> RABBITMQ: NOTIFICATION SENT TO: {EmailTo}", data.EmailTo);
            }));
    }

    private async Task Execute<T>(Func<T, Task> action)
    {
        using var scope = _serviceProvider.CreateScope();

        var service = scope.ServiceProvider.GetService<T>();

        if (service == null)
        {
            _logger.LogError("--> Error: {Service} not found", service);

            throw new NullReferenceException($"Specified service <{typeof(T)}> not found");
        }

        await action(service);
    }
}
