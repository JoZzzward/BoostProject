using BoostProject.Common.Consts;
using BoostProject.Services.EmailSender;
using BoostProject.Services.RabbitMqService;

namespace BoostProject.Services.Actions;

public class ActionsService : IActionsService
{
    private readonly IRabbitMqService _rabbitMqService;

    public ActionsService(
        IRabbitMqService rabbitMqService
        )
    {
        _rabbitMqService = rabbitMqService;
    }

    public async Task SendEmail(EmailModel model)
    {
        await _rabbitMqService.PushAsync(ActionConsts.SEND_EMAIL_NOTIFICATION, model);
    }
}
