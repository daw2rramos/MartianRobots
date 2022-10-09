using MartianRobots.API.Models;
using MartianRobots.Application.Maps.DTOs;

namespace MartianRobots.API.Mappers
{
    public static class MapMappers
    {
        public static MapDto AsDto(this MapModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new MapDto(model.Id, model.Name, model.MarsWidth, model.MarsHeight);
        }
    }
}
