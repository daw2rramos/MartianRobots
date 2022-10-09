using MartianRobots.Application.Robots.DTOs;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Robots.Queries
{
    public class GetRobotReportQuery : IRequest<RobotReportDto>
    {
        public GetRobotReportQuery(Guid robotId)
        {
            this.RobotId = Guards.ThrowIfNull(robotId);
        }

        public Guid RobotId { get; set; }
    }
}
