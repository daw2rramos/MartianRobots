using MartianRobots.Application.Maps.DTOs;
using MartianRobots.Domain.MapsAggregate;

namespace MartianRobots.Application.Maps.Mappers
{
    public static class MapMappers
    {
        public static MapDto AsDto(this Map map)
        {
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            return new MapDto(map.Id, map.Name, map.Width, map.Height);
        }
    }
}
