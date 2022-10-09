using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MartianRobots.Application.Maps.DTOs;
using MartianRobots.Application.Maps.Repositories;
using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace MartianRobots.Infrastructure.Repositories
{
    public class MapRepository : IMapRepository
    {
        private readonly MartianRobotsContext context;

        public MapRepository(MartianRobotsContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => this.context;

        public async Task<IReadOnlyList<Map>> GetAsync(CancellationToken cancellationToken = default)
        {
            var mapsCollection = await this.context.Maps
            .Include(map => map.Cells)
            .AsNoTracking()
               .ToListAsync(cancellationToken)
               .ConfigureAwait(false);

            return mapsCollection;
        }

        public async Task<Map?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //return await this.context.Maps
            //    .Include(map => map.Cells)
            //    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            //    .ConfigureAwait(false);
            var map = await this.context.Maps.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);

            if(map is null)
            {
                return default(Map?);
            }

            await this.context.Entry(map).Collection(x => x.Cells).LoadAsync(cancellationToken).ConfigureAwait(false);

            return map;
        }
    }
}
