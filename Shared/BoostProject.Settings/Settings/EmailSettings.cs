using BoostProject.Settings.Interfaces;
using BoostProject.Settings.Source;

namespace BoostProject.Settings.Settings;

public class EmailSettings : IEmailSettings
{
    private readonly ISettingSource _source;

    public EmailSettings(ISettingSource source)
    {
        _source = source;
    }

    public string Host => _source.GetAsString("EmailSettings:Host");
    public int Port => _source.GetAsInt("EmailSettings:Port");
    public string Login => _source.GetAsString("EmailSettings:Login");
    public string Password => _source.GetAsString("EmailSettings:Password");
}