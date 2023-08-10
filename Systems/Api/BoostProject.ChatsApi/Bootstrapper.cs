using BoostProject.Services.MessagesService;

namespace BoostProject.ChatsApi;

public static class Bootstrapper
{
    internal static void RegisterAppServices(this IServiceCollection services)
    {
        services.AddMessagesService()
            
            ;
    }
}
