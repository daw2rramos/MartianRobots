using MartianRobots.Application.Robots.DTOs;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Robots.Commands
{
    public class MoveCommand : IRequest<Result>
    {
        public MoveCommand(Guid robotId, MoveRobotDto instruction)
        {
            this.RobotId = Guards.ThrowIfNull(robotId);
            this.MovementInstructions = Guards.ThrowIfNull(instruction);
        }

        public Guid RobotId { get; set; }

        public MoveRobotDto? MovementInstructions { get; set; }
    }
}
