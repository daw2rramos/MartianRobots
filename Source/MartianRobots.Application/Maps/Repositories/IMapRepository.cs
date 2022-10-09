using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Application.Maps.Repositories
{
    public interface IMapRepository : IRepository<Map>
    {
        Task<IReadOnlyList<Map>> GetAsync(CancellationToken cancellation = default);
    }
}
