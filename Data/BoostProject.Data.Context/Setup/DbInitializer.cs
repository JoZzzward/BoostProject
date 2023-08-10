using BoostProject.Common.Enums;
using BoostProject.Data.Entities.AppUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BoostProject.Data.Context;

/// <summary>
/// Database initializer
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Initializing database by latest migration
    /// </summary>
    public static async Task Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

        using var context = dbContextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}
