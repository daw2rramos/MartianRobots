using MartianRobots.Application.Maps.DTOs;
using MartianRobots.Application.Maps.Mappers;
using MartianRobots.Application.Maps.Repositories;
using MartianRobots.SharedKernel;
using MediatR;

namespace MartianRobots.Application.Maps.Queries
{
    public class GetMapQueryHandler : IRequestHandler<GetMapQuery, MapDto>
    {
        private readonly IMapRepository mapRepository;

        public GetMapQueryHandler(IMapRepository mapRepository)
        {
            this.mapRepository = Guards.ThrowIfNull(mapRepository);
        }

        public async Task<MapDto> Handle(GetMapQuery request, CancellationToken cancellationToken)
        {
            var availableMaps = await this.mapRepository.GetAsync(cancellationToken).ConfigureAwait(false);

            var map = availableMaps.FirstOrDefault(x => x.Name == request.MapName);

            if (map is null)
            {
                throw new ArgumentNullException($"Map with name: {request.MapName} not found");
            }

            return map.AsDto();
        }
    }
}
