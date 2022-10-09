using Microsoft.EntityFrameworkCore;

namespace MartianRobots.API.Seed
{
    public static class MigrationExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        where TContext : DbContext
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            services.MigrateDbContext<TContext>();

            return host;
        }

        private static void MigrateDbContext<TContext>(this IServiceProvider services)
            where TContext : DbContext
        {
            using var context = services.GetRequiredService<TContext>();
            var migrationsNeeded = context.Database.GetPendingMigrations().Any();
            if (!migrationsNeeded)
            {
                return;
            }

            context.Database.Migrate();
        }
    }
}
