using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.RobotsAggregate.Services
{
    public interface IMovementService
    {
        Result Move(Robot robot, Map map, Direction instruction);
    }
}
