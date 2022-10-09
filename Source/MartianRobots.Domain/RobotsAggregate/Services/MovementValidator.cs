using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.RobotsAggregate.Services
{
    public class MovementValidator : IMovementValidator
    {
        private readonly IRobotMovement robotMovement;

        public MovementValidator(IRobotMovement robotMovement)
        {
            this.robotMovement = Guards.ThrowIfNull(robotMovement); ;
        }

        public Result<Coordinates> Validate(Robot robot, Map map, Direction instruction)
        {
            Guards.ThrowIfNull(robot);
            Guards.ThrowIfNull(map);                      

            return robotMovement.CheckMove(robot, map, instruction);
        }
    }
}
