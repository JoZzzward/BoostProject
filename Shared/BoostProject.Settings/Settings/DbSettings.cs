using BoostProject.Settings.Interfaces;
using BoostProject.Settings.Source;

namespace BoostProject.Settings.Settings;

public class DbSettings : IDbSettings
{
    private readonly ISettingSource _source;

    public DbSettings(ISettingSource source)
    {
        _source = source;
    }

    public string ConnectionString => _source.GetAsString("Database:ConnectionString");
}