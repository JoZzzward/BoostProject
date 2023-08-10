using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace BoostProject.Settings.Source;

public partial class SettingSource : ISettingSource
{
    private readonly IConfiguration? _configuration;

    public SettingSource(IConfiguration? configuration = null)
    {
        _configuration = configuration ?? new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }

    public string GetConnectionString(string? source = null)
    {
        return ApplyEnvironmentVariable(_configuration!.GetConnectionString(source ?? "ConnectionString") ?? "");
    }

    public string? GetAsString(string source, string? defaultValue = null)
    {
        return ApplyEnvironmentVariable(_configuration[source]) ?? defaultValue;
    }

    public bool GetAsBool(string source, bool defaultValue = false)
    {
        var val = ApplyEnvironmentVariable(_configuration[source]);
        return bool.TryParse(val, out var result) ? result : defaultValue;
    }

    public int GetAsInt(string source, int defaultValue = 0)
    {
        var val = ApplyEnvironmentVariable(_configuration[source]);
        return int.TryParse(val, out var result) ? result : defaultValue;
    }

    private string ApplyEnvironmentVariable(string value)
    {
        value ??= "";
        var list = MyRegex().Matches(value).OfType<Match>().Select(c => c.Value).ToList();

        foreach (var item in list)
        {
            var v = item.Replace("{", "").Replace("}", "");
            value = value.Replace($"{{{v}}}", _configuration[v]);
        }

        return value;
    }

    [GeneratedRegex("{[^}]+}")]
    private static partial Regex MyRegex();
}