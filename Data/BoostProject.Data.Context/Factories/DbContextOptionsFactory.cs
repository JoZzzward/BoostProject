using BoostProject.Settings.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoostProject.Data.Context.Factories;

internal static class DbContextOptionsFactory
{
    internal static Action<DbContextOptionsBuilder> Configure(IDbSettings settings)
    {
        return (bldr) => {
            bldr.UseNpgsql(
                    settings.ConnectionString,
                    o =>
                    {
                        o.CommandTimeout((int) TimeSpan.FromMinutes(10).TotalSeconds);
                        o.MigrationsHistoryTable("EFMigrationHistory", "public");
                    });
            bldr.EnableSensitiveDataLogging();
            bldr.EnableDetailedErrors();
            bldr.UseLazyLoadingProxies();
            bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            bldr.UseOpenIddict<Guid>();
        };
    }
}