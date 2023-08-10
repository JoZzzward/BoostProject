namespace BoostProject.Settings.Interfaces;

public interface IEmailSettings
{
    string Host { get; }
    int Port { get; }
    string Login { get; }
    string Password { get; }
}