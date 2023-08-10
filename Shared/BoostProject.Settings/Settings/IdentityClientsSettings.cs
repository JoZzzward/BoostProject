using BoostProject.Settings.Interfaces;
using BoostProject.Settings.Source;

namespace BoostProject.Settings.Settings;

public class IdentityClientsSettings : IIdentityClientsSettings
{

    private readonly ISettingSource _source;

    public IdentityClientsSettings(ISettingSource source)
    {
        _source = source;
    }

    public string ApiClientId =>  _source.GetAsString("IdentityClientsSettings:ApiClientId");
    public string ApiClientSecret =>  _source.GetAsString("IdentityClientsSettings:ApiClientSecret");
    public string ApiRedirectUri => _source.GetAsString("IdentityClientsSettings:ApiRedirectUri");
    public string ApiPostLogoutRedirectUri => _source.GetAsString("IdentityClientsSettings:ApiPostLogoutRedirectUri");

    public string ChatsApiClientId =>  _source.GetAsString("IdentityClientsSettings:ChatsApiClientId");
    public string ChatsApiClientSecret =>  _source.GetAsString("IdentityClientsSettings:ChatsApiClientSecret");
    public string ChatsApiRedirectUri => _source.GetAsString("IdentityClientsSettings:ChatsApiRedirectUri");
    public string ChatsApiPostLogoutRedirectUri => _source.GetAsString("IdentityClientsSettings:ChatsApiPostLogoutRedirectUri");

    public string WebClientId =>  _source.GetAsString("IdentityClientsSettings:WebClientId");
    public string WebClientSecret =>  _source.GetAsString("IdentityClientsSettings:WebClientSecret");
    public string WebRedirectUri => _source.GetAsString("IdentityClientsSettings:WebRedirectUri");
    public string WebPostLogoutRedirectUri => _source.GetAsString("IdentityClientsSettings:WebPostLogoutRedirectUri");

    public string ResourceOwnerClientId =>  _source.GetAsString("IdentityClientsSettings:ResourceOwnerClientId");
    public string ResourceOwnerClientSecret =>  _source.GetAsString("IdentityClientsSettings:ResourceOwnerClientSecret");
    public string ResourceOwnerRedirectUri => _source.GetAsString("IdentityClientsSettings:ResourceOwnerRedirectUri");
    public string ResourceOwnerPostLogoutRedirectUri => _source.GetAsString("IdentityClientsSettings:ResourceOwnerPostLogoutRedirectUri");
}