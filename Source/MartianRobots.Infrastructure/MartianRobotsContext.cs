using MartianRobots.Domain.MapsAggregate;
using MartianRobots.Domain.RobotsAggregate;
using MartianRobots.Infrastructure.Configurations;
using MartianRobots.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace MartianRobots.Infrastructure
{
    public sealed class MartianRobotsContext : DbContext, IUnitOfWork
    {
        public MartianRobotsContext(DbContextOptions options)
        : base(options)
        {           
        }

        public DbSet<Robot> Robots { get; set; } = default!;

        public DbSet<Map> Maps { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guards.ThrowIfNull(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RobotEntityTypeConfiguration).Assembly);
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {            
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }
       
        public override async ValueTask DisposeAsync()
        {           
            await base.DisposeAsync().ConfigureAwait(false);
        }
    }
}
