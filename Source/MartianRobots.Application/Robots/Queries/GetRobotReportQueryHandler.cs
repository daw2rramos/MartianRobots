using MartianRobots.Application.Robots.DTOs;
using MartianRobots.Application.Robots.Mappers;
using MartianRobots.Application.Robots.Repositories;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Robots.Queries
{
    public class GetRobotReportQueryHandler : IRequestHandler<GetRobotReportQuery, RobotReportDto>
    {
        private readonly IRobotRepository robotRepository;

        public GetRobotReportQueryHandler(IRobotRepository robotRepository)
        {
            this.robotRepository = Guards.ThrowIfNull(robotRepository);
        }

        public async Task<RobotReportDto> Handle(GetRobotReportQuery request, CancellationToken cancellationToken)
        {
            var robot = await this.robotRepository.GetAsync(request.RobotId, cancellationToken).ConfigureAwait(false);            

            if (robot is null)
            {
                throw new ArgumentNullException($"Robot with id: {request.RobotId} not found");
            }

            return robot.AsReport();
        }
    }
}
