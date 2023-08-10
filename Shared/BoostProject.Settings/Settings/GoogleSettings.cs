using BoostProject.Settings.Interfaces;
using BoostProject.Settings.Source;

namespace BoostProject.Settings.Settings;

public class GoogleSettings : IGoogleSettings
{
    private readonly ISettingSource _source;

    public GoogleSettings(ISettingSource source)
    {
        _source = source;
    }

    public string ClientId => _source.GetAsString("GoogleSettings:ClientId");
    public string ClientSecret => _source.GetAsString("GoogleSettings:ClientSecret");
}