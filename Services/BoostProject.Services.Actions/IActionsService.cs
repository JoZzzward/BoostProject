using BoostProject.Services.EmailSender;

namespace BoostProject.Services.Actions;

public interface IActionsService
{
    Task SendEmail(EmailModel model);
}