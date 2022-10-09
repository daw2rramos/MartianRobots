using MartianRobots.Application.Robots.DTOs;
using MartianRobots.Application.Robots.Mappers;
using MartianRobots.Application.Robots.Repositories;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Robots.Queries
{
    public class GetRobotQueryHandler : IRequestHandler<GetRobotQuery, RobotDto>
    {
        private readonly IRobotRepository robotRepository;

        public GetRobotQueryHandler(IRobotRepository robotRepository)
        {
            this.robotRepository = Guards.ThrowIfNull(robotRepository);
        }

        public async Task<RobotDto> Handle(GetRobotQuery request, CancellationToken cancellationToken)
        {           
            var availableRobots = await this.robotRepository.GetAsync(cancellationToken).ConfigureAwait(false);

            var robot = availableRobots.FirstOrDefault(x => x.Name == request.RobotName);

            if (robot is null)
            {
                throw new ArgumentNullException($"Robot with name: {request.RobotName} not found");
            }

            return robot.AsDto();
        }
    }
}
