using BoostProject.Settings.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace BoostProject.Services.EmailSender.Management;

public class Manager
{
    private readonly ILogger<EmailSender> _logger;
    private readonly IEmailSettings _settings;

    protected Manager(
        ILogger<EmailSender> logger,
        IEmailSettings settings
        )
    {
        _logger = logger;
        _settings = settings;
    }

    protected MimeMessage InitializeMessage(EmailModel model)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_settings.Login));
        email.To.Add(MailboxAddress.Parse(model.EmailTo));
        email.Subject = model.Subject;

        email.Body = new TextPart(TextFormat.Html)
        {
            Text = model.Message
        };

        _logger.LogInformation("--> Message for specified email(Email: {EmailTo}) is initialized.}", model.EmailTo);

        return email;
    }

    protected void SendMessage(MimeMessage email)
    {
        using var smtp = new SmtpClient();
        smtp.Connect(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
        smtp.Authenticate(_settings.Login, _settings.Password);
        smtp.Send(email);
        smtp.Disconnect(true);

        _logger.LogInformation("--> Email sended to: {EmailTo}; ", email.To);
    }
}
