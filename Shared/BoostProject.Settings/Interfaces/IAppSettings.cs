namespace BoostProject.Settings.Interfaces;

public interface IAppSettings
{
    IDbSettings Db { get; }
    IIdentitySettings Identity { get; }
    IIdentityClientsSettings IdentityClients { get; }
    IEmailSettings Email { get; }
    IRedisSettings Redis { get; }
    IRabbitMqSettings Rabbit { get; }
    IGoogleSettings Google { get; }
    IVkontakteSettings Vkontakte { get; }
}