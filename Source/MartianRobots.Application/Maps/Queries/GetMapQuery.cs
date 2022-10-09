using MartianRobots.Application.Maps.DTOs;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Maps.Queries
{
    public class GetMapQuery : IRequest<MapDto>
    {
        public GetMapQuery(string mapName)
        {
            this.MapName = Guards.ThrowIfNullOrEmpty(mapName);
        }

        public string MapName { get; set; }
    }
}
