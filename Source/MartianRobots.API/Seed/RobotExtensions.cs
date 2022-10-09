using MartianRobots.Domain.MapsAggregate;
using MartianRobots.Domain.RobotsAggregate;
using MartianRobots.Infrastructure;

namespace MartianRobots.API.Seed
{
    public static class RobotExtensions
    {
        public static IHost LoadRobot(this IHost host)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MartianRobotsContext>();

            if (context.Robots.Any())
            {
                return host;
            }

            context.Robots.Add(new Robot("Perseverance"));
            context.SaveChanges();

            return host;
        }
    }
}
