using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MartianRobots.Infrastructure.Configurations
{
    public class CellEntityTypeConfiguration : IEntityTypeConfiguration<Cell>
    {
        public void Configure(EntityTypeBuilder<Cell> builder)
        {
            Guards.ThrowIfNull(builder);

            builder.OwnsOne(x => x.Coordinates, y =>
            {
                y.Property(z => z.XPos).HasColumnName("xPos");
                y.Property(z => z.YPos).HasColumnName("yPos");
            });
        }
    }
}
