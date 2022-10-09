using MartianRobots.Domain.MapsAggregate;
using MartianRobots.Infrastructure;

namespace MartianRobots.API.Seed
{
    public static class MapExtensions
    {
        public static IHost LoadMap(this IHost host)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MartianRobotsContext>();

            if (context.Maps.Any())
            {
                return host;
            }           

            context.Maps.Add(new Map("Jezero Crater", MapSeed.GetDefaultMapCellConfiguration()));
            context.SaveChanges();

            return host;
        }
    }
}
