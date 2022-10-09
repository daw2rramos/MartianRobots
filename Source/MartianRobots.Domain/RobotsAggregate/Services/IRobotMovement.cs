using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.RobotsAggregate.Services
{
    public interface IRobotMovement
    {
        Result<Coordinates> CheckMove(Robot robot, Map map, Direction instruction);
    }
}
