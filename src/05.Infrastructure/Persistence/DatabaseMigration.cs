using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Zeta.CodebaseExpress.Infrastructure.Persistence;

public static class DatabaseMigration
{
    public static async Task ApplyDatabaseMigrationAsync<T>(this IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<T>>();
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var isMigrationNeeded = (await context.Database.GetPendingMigrationsAsync()).Any();

        if (isMigrationNeeded)
        {
            logger.LogInformation("Applying database migration...");
            context.Database.Migrate();
        }
        else
        {
            logger.LogInformation("Database is up to date. No database migration required.");
        }
    }
}
