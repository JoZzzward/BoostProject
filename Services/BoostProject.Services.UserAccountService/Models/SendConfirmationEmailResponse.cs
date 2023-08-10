using System.Text.Json.Serialization;

namespace BoostProject.Services.UserAccountService.Models;

public class SendConfirmationEmailResponse
{
    public string Email { get; set; }
}
