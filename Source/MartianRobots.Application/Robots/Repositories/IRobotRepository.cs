using MartianRobots.Application.Robots.DTOs;
using MartianRobots.Domain.RobotsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Application.Robots.Repositories
{
    public interface IRobotRepository : IRepository<Robot>
    {
        Task<IReadOnlyList<Robot>> GetAsync(CancellationToken cancellationToken = default);
    }
}
