namespace BoostProject.Settings.Interfaces;

public interface IIdentityClientsSettings
{
    string ApiClientId { get; }
    string ApiClientSecret { get; }
    string ApiRedirectUri { get; }
    string ApiPostLogoutRedirectUri { get; }

    string ChatsApiClientId { get; }
    string ChatsApiClientSecret { get; }
    string ChatsApiRedirectUri { get; }
    string ChatsApiPostLogoutRedirectUri { get; }

    string WebClientId { get; }
    string WebClientSecret { get; }
    string WebRedirectUri { get; }
    string WebPostLogoutRedirectUri { get; }

    string ResourceOwnerClientId { get; }
    string ResourceOwnerClientSecret { get; }
    string ResourceOwnerRedirectUri { get; }
    string ResourceOwnerPostLogoutRedirectUri { get; }
}