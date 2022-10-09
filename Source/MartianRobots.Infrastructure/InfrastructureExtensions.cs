using MartianRobots.Application.Maps.Repositories;
using MartianRobots.Application.Robots.Repositories;
using MartianRobots.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {            
            services.AddScoped<IMapRepository, MapRepository>();
            services.AddScoped<IRobotRepository, RobotRepository>();

            return services;
        }

        public static IServiceCollection AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MartianRobotsContext>(options =>
            {
                options.UseSqlServer(connectionString,
                        sqlOptions => { 
                            sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null); 
                        })
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });

            return services;
        }
    }
}
