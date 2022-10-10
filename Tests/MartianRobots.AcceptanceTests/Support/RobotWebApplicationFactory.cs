using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.API;
using MartianRobots.API.Seed;
using MartianRobots.Domain.MapsAggregate;
using MartianRobots.Domain.RobotsAggregate;
using MartianRobots.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.AcceptanceTests.Support
{
    public class RobotWebApplicationFactory : BaseWebApplicationFactory<Startup>
    {
        private Guid defaultMapId;
        private Guid defaultRobotId;
        private MartianRobotsContext dbContext;
        private IDbContextFactory<MartianRobotsContext> dbContextFactory;
        private bool disposed;

        private MartianRobotsContext DbContext => this.dbContext ??=
            this.ServiceProvider.GetRequiredService<MartianRobotsContext>();

        public IDbContextFactory<MartianRobotsContext> DbContextFactory =>
            this.dbContextFactory ??= this.ServiceProvider.GetRequiredService<IDbContextFactory<MartianRobotsContext>>();

        public Guid DefaultRobotId
        {
            get
            {
                if (this.defaultRobotId == Guid.Empty)
                {
                    this.defaultRobotId = this.DbContext.Robots.First().Id;
                }

                return this.defaultRobotId;
            }
        }

        public Guid DefaultMapId
        {
            get
            {
                if (this.defaultMapId == Guid.Empty)
                {
                    this.defaultMapId = this.DbContext.Maps.First().Id;
                }

                return this.defaultMapId;
            }
        }

        protected override void SeedDatabase(MartianRobotsContext context)
        {
            context.Maps.Add(new Map("Test Area", MapSeed.GetDefaultMapCellConfiguration()));
            context.Robots.Add(new Robot("Test robot"));
            context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.DbContext.Database.EnsureDeleted();
                this.DbContext.Dispose();
                this.disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
