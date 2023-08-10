using BoostProject.AuthorizationServer.ClientsSeeder.Clients;
using BoostProject.Common.Security;
using BoostProject.Settings.Interfaces;
using OpenIddict.Abstractions;

namespace BoostProject.AuthorizationServer.Clients;

public class ClientsSeeder
{
    private readonly IServiceProvider _serviceProvider;

    public ClientsSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task AddScopes()
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        var scopes = AppScopesManager.GetAllScopes();

        foreach (var item in scopes)
        {
            if (await manager.FindByNameAsync(item) is null)
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    DisplayName = item,
                    Name = item,
                    Resources =
                    {
                        item
                    }
                });
        }
    }

    public async Task AddClients(IAppSettings settings)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        await ApiClient.AddClient(manager, settings);
        await ChatsApiClient.AddClient(manager, settings);
        await WebAppClient.AddClient(manager, settings);
        await ResourceOwnerClient.AddClient(manager, settings);
    }
}
