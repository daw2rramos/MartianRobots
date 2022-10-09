using MartianRobots.Application.Maps.Converters;
using MartianRobots.Application.Maps.Repositories;
using MartianRobots.Application.Robots.Repositories;
using MartianRobots.Domain.RobotsAggregate.Services;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Robots.Commands
{
    public class MoveCommandHandler : IRequestHandler<MoveCommand, Result>
    {
        private readonly IRobotRepository robotRepository;
        private readonly IMapRepository mapRepository;
        private readonly IMovementService moveService;

        public MoveCommandHandler(IRobotRepository robotRepository, IMapRepository mapRepository, IMovementService movementService)
        {
            this.robotRepository = Guards.ThrowIfNull(robotRepository);
            this.mapRepository = Guards.ThrowIfNull(mapRepository);
            this.moveService = Guards.ThrowIfNull(movementService);
        }

        public async Task<Result> Handle(MoveCommand request, CancellationToken cancellationToken)
        {
            var movementInstructions = request.MovementInstructions;

            if(movementInstructions is null)
            {
                return Result.Fail($"Required data to perform the movement not found");
            }

            var robot = await this.robotRepository.GetAsync(request.RobotId, cancellationToken).ConfigureAwait(false);
            if (robot is null)
            {
                return Result.Fail($"Robot with id: {request.RobotId} not found");
            }

            var map = await this.mapRepository.GetAsync(movementInstructions.MapId, cancellationToken).ConfigureAwait(false);
            if(map is null)
            {
                return Result.Fail($"Map with id: {movementInstructions.MapId} not found");
            }

            var instructions = StringToDirectionCollection.Convert(movementInstructions.Instructions);

            foreach(var instruction in instructions)
            {
                var movementSucceed = this.moveService.Move(robot, map, instruction);                                

                if (movementSucceed.Success)
                {
                    continue;
                }

                await this.robotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return Result.Fail(""); //Robot cannot follow the instruction
            }

            var isRobotMovementSucceed = await this.robotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!isRobotMovementSucceed)
            {
                return Result.Fail(""); //Changes cannot be applied
            }

            return Result.Ok();
        }
    }
}
