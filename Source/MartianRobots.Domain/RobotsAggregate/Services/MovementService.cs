using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.RobotsAggregate.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementValidator movementValidator;

        public MovementService(IMovementValidator movementValidator)
        {
            this.movementValidator = Guards.ThrowIfNull(movementValidator);
        }

        public Result Move(Robot robot, Map map, Direction instruction)
        {
            Guards.ThrowIfNull(robot);
            Guards.ThrowIfNull(map);

            var result = this.movementValidator.Validate(robot, map, instruction);
            if (result.Success)
            {
                robot.MoveTo(result.Value);
            }

            return result;
        }
    }
}
