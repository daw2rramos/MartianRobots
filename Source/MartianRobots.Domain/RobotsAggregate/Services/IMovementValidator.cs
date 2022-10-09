using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.RobotsAggregate.Services
{
    public interface IMovementValidator
    {
        Result<Coordinates> Validate(Robot robot, Map map, Direction instruction);
    }
}
