namespace BoostProject.Services.EmailSender;

public interface IEmailSender
{
    Task SendEmail(EmailModel model);
}