using BoostProject.Settings.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BoostProject.AuthorizationServer.Configuration.IdentitySettings;

public static class Encryptions
{
    public static void EnableEncryptions(this OpenIddictServerBuilder options, IAppSettings settings)
    {
        options.AddEncryptionKey(new SymmetricSecurityKey(
            Convert.FromBase64String(settings.Identity.SigningKey)));

        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();
    }
}
