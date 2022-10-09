using MartianRobots.Domain.MapsAggregate;
using MartianRobots.Domain.MapsAggregate.Services;
using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.RobotsAggregate.Services
{
    public class RobotMovement : IRobotMovement
    {
        private readonly IMapService mapServices;

        public RobotMovement(IMapService mapServices)
        {
            this.mapServices = Guards.ThrowIfNull(mapServices);
        }

        public Result<Coordinates> CheckMove(Robot robot, Map map, Direction instruction)
        {
            Guards.ThrowIfNull(robot);
            Guards.ThrowIfNull(map);

            var result = instruction switch
            {
                Direction.Forward => MoveForwardAction(robot, map),                
                Direction.Left => TurnLeftAction(robot, map),
                Direction.Right => TurnRightAction(robot, map),
                _ => Result.Fail<Coordinates>($"Direction {nameof(instruction)} not available")
            };

            return result;
        }

        private static Result<Coordinates> TurnLeftAction(Robot robot, Map map)
        {
            if (robot.Coordinates is null)
            {
                return Result.Fail<Coordinates>(""); //robot is not deployed
            }

            robot.TurnLeft(robot.Orientation);            

            return robot.CanMove(map, 0, 0);
        }

        private static Result<Coordinates> TurnRightAction(Robot robot, Map map)
        {
            if (robot.Coordinates is null)
            {
                return Result.Fail<Coordinates>(""); //robot is not deployed
            }

            robot.TurnRight(robot.Orientation);
            
            return robot.CanMove(map, 0, 0);
        }   
        
        private Result<Coordinates> MoveForwardAction(Robot robot, Map map)
        {
            // Troubled robots can't move
            //if (robot.IsLost()) return;

            if (robot.Coordinates is null)
            {
                return Result.Fail<Coordinates>(""); //robot is not deployed
            }

            var nextSteps = this.mapServices.GetNextMovement(robot.Coordinates, robot.Orientation);

            if (nextSteps.Success)
            {
                return robot.CanMove(map, nextSteps.Value.xStep, nextSteps.Value.yStep);                
            }

            return Result.Fail<Coordinates>($"Error calculating the next movement => {nextSteps.Error}");
        }
    }
}
