using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Domain.RobotsAggregate.Services;
using MartianRobots.Domain.MapsAggregate.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Domain
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<IRobotMovement, RobotMovement>();
            services.AddSingleton<IMapService, MapService>();
            services.AddSingleton<IMovementValidator, MovementValidator>();
            services.AddSingleton<IMovementService, MovementService>();

            return services;
        }
    }
}
