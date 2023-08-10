using BoostProject.Settings.Interfaces;
using BoostProject.Settings.Source;

namespace BoostProject.Settings.Settings;

public class AppSettings : IAppSettings
{
    private readonly IDbSettings _db;
    private readonly ISettingSource _source;
    private readonly IIdentitySettings _identity;
    private readonly IIdentityClientsSettings _identityClients;
    private readonly IEmailSettings _email;
    private readonly IRedisSettings _redis;
    private readonly IRabbitMqSettings _rabbit;
    private readonly IGoogleSettings _google;
    private readonly IVkontakteSettings _vkontakte;

    public AppSettings(ISettingSource source)
    {
        _source = source;
    }

    public AppSettings(
        IDbSettings db, 
        ISettingSource source,
        IIdentitySettings identity,
        IIdentityClientsSettings identityClients,
        IEmailSettings email,
        IRedisSettings redis,
        IRabbitMqSettings rabbit,
        IGoogleSettings google,
        IVkontakteSettings vkontakte)
    {
        _db = db;
        _source = source;
        _identity = identity;
        _identityClients = identityClients;
        _email = email;
        _redis = redis;
        _rabbit = rabbit;
        _google = google;
        _vkontakte = vkontakte;
    }
    
    public IDbSettings Db => _db ?? new DbSettings(_source);
    public IIdentitySettings Identity => _identity ?? new IdentitySettings(_source);
    public IIdentityClientsSettings IdentityClients => _identityClients ?? new IdentityClientsSettings(_source);
    public IEmailSettings Email => _email ?? new EmailSettings(_source);
    public IRedisSettings Redis => _redis ?? new RedisSettings(_source);
    public IRabbitMqSettings Rabbit => _rabbit ?? new RabbitMqSettings(_source);
    public IGoogleSettings Google => _google ?? new GoogleSettings(_source);
    public IVkontakteSettings Vkontakte => _vkontakte ?? new VkontakteSettings(_source);
}