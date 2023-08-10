using BoostProject.Settings.Interfaces;
using BoostProject.Settings.Source;

namespace BoostProject.Settings.Settings;

public class RabbitMqSettings : IRabbitMqSettings
{
    private readonly ISettingSource _source;

    public RabbitMqSettings(ISettingSource source)
    {
        _source = source;
    }

    public string Uri => _source.GetAsString("RabbitMqSettings:Uri");
    public string UserName => _source.GetAsString("RabbitMqSettings:UserName");
    public string Password => _source.GetAsString("RabbitMqSettings:Password");
}