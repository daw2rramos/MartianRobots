using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.MapsAggregate.Services
{
    public interface IMapService
    {        
        Result<(int xStep, int yStep)> GetNextMovement(Coordinates coordinates, Orientation orientation);
    }
}
