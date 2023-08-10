using BoostProject.Services.EmailSender.Management;
using BoostProject.Settings.Interfaces;
using Microsoft.Extensions.Logging;

namespace BoostProject.Services.EmailSender;

public class EmailSender : Manager, IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(
        ILogger<EmailSender> logger,
        IEmailSettings settings
        ) : base(logger, settings)
    {
        _logger = logger;
    }

    public async Task SendEmail(EmailModel model)
    {
        _logger.LogInformation("--> Trying to send email to {EmailTo}", model.EmailTo);

        var email = InitializeMessage(model);

        SendMessage(email);
    }
}
