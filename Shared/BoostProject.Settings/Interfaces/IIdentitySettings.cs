namespace BoostProject.Settings.Interfaces;

public interface IIdentitySettings
{
    string Url { get; }
    string ClientId { get; }
    string ClientSecret { get; }
    string SigningKey { get; }
    bool RequireHttps { get; }

    /// <summary>
    /// Lifetime in days
    /// </summary>
    int AccessTokenLifetime { get; }

    /// <summary>
    /// Lifetime in days
    /// </summary>
    int RefreshTokenLifetime { get; }
}