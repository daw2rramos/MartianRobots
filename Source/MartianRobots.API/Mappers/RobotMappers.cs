using MartianRobots.API.Models;
using MartianRobots.Application.Robots.DTOs;

namespace MartianRobots.API.Mappers
{
    public static class RobotMappers
    {
        public static PlaceRobotDto AsDto(this PlaceRobotModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new PlaceRobotDto(model.MapId, model.DeployInstruction);
        }

        public static MoveRobotDto AsDto(this MoveRobotModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new MoveRobotDto(model.MapId, model.Instruction);
        }
    }
}
