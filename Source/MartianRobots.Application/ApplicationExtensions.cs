using MartianRobots.Application.Maps.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetMapQuery).Assembly);

            return services;
        }
    }
}
