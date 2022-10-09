using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Application.Robots.DTOs;
using MartianRobots.Application.Robots.Repositories;
using MartianRobots.Domain.RobotsAggregate;
using MartianRobots.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace MartianRobots.Infrastructure.Repositories
{
    public class RobotRepository : IRobotRepository
    {
        private readonly MartianRobotsContext context;

        public RobotRepository(MartianRobotsContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => this.context;

        public async Task<Robot?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await this.context.Robots
                .FirstOrDefaultAsync(y => y.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<Robot>> GetAsync(CancellationToken cancellationToken = default)
        {
            var robotsCollection = await this.context.Robots
                .AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return robotsCollection;
        }
    }
}
