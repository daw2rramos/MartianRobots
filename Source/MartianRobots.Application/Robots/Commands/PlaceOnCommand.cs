using MartianRobots.Application.Robots.DTOs;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Robots.Commands
{
    public class PlaceOnCommand : IRequest<Result>
    {
        public PlaceOnCommand(Guid robotId, PlaceRobotDto instruction)
        {
            this.RobotId = Guards.ThrowIfNull(robotId);
            this.DeployInstruction = Guards.ThrowIfNull(instruction);
        }        

        public Guid RobotId { get; private set; }
        public PlaceRobotDto DeployInstruction { get; private set; }        
    }
}
