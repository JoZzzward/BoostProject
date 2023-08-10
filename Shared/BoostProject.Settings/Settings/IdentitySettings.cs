using BoostProject.Settings.Interfaces;
using BoostProject.Settings.Source;

namespace BoostProject.Settings.Settings;

public class IdentitySettings : IIdentitySettings
{
    private readonly ISettingSource _source;

    public IdentitySettings(ISettingSource source)
    {
        _source = source;
    }

    public string Url => _source.GetAsString("IdentitySettings:Url");

    public string ClientId => _source.GetAsString("IdentitySettings:ClientId");

    public string ClientSecret => _source.GetAsString("IdentitySettings:ClientSecret");

    public bool RequireHttps => Url.ToLower().StartsWith("https://");

    public string SigningKey => _source.GetAsString("IdentitySettings:SigningKey");

    public int AccessTokenLifetime => _source.GetAsInt("IdentitySettings:AccessTokenLifetime");

    public int RefreshTokenLifetime => _source.GetAsInt("IdentitySettings:RefreshTokenLifetime");
}