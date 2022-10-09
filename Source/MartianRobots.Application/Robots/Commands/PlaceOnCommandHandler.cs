using MartianRobots.Application.Maps.Converters;
using MartianRobots.Application.Maps.Repositories;
using MartianRobots.Application.Robots.Repositories;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Robots.Commands
{
    public class PlaceOnCommandHandler : IRequestHandler<PlaceOnCommand, Result>
    {
        private readonly IRobotRepository robotRepository;
        private readonly IMapRepository mapRepository;

        public PlaceOnCommandHandler(IRobotRepository robotRepository, IMapRepository mapRepository)
        {
            this.robotRepository = Guards.ThrowIfNull(robotRepository);
            this.mapRepository = Guards.ThrowIfNull(mapRepository);
        }

        public async Task<Result> Handle(PlaceOnCommand request, CancellationToken cancellationToken)
        {
            var instruction = request.DeployInstruction;

            if (instruction is null)
            {
                return Result.Fail($"Robot cannot be placed on the map due to there are not concrete instructions.");
            }

            var robot = await this.robotRepository.GetAsync(request.RobotId, cancellationToken).ConfigureAwait(false);
            if (robot is null)
            {
                return Result.Fail($"Robot with id: {request.RobotId} not found");
            }

            var map = await this.mapRepository.GetAsync(instruction.MapId, cancellationToken).ConfigureAwait(false);
            if (map is null)
            {
                return Result.Fail($"Map with id: {instruction.MapId} not found");
            }

            var deployInstruction = StringToDeployInstruction.Convert(instruction.DeployInstruction);

            robot.PlaceOn(map, deployInstruction.Coordinates.XPos, deployInstruction.Coordinates.YPos, deployInstruction.Orientation);

            var isRobotDeployed = await this.robotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

            if (!isRobotDeployed)
            {
                return Result.Fail(""); //Changes cannot be applied               
            }

            return Result.Ok();
        }           
    }
}
