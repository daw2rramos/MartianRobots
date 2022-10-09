using MartianRobots.Application.Robots.DTOs;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Robots.Queries
{
    public class GetRobotQuery : IRequest<RobotDto>
    {
        public GetRobotQuery(string robotName)
        {
            this.RobotName = Guards.ThrowIfNullOrEmpty(robotName);
        }

        public string RobotName { get; set; }
    }
}
