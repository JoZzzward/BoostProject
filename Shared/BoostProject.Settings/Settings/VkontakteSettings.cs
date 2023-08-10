using BoostProject.Settings.Interfaces;
using BoostProject.Settings.Source;

namespace BoostProject.Settings.Settings;

public class VkontakteSettings : IVkontakteSettings
{
    private readonly ISettingSource _source;

    public VkontakteSettings(ISettingSource source)
    {
        _source = source;
    }

    public string ClientId => _source.GetAsString("VkontakteSettings:ClientId");
    public string ClientSecret => _source.GetAsString("VkontakteSettings:ClientSecret");
}