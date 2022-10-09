using MartianRobots.Domain.RobotsAggregate;
using MartianRobots.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartianRobots.Infrastructure.Configurations
{
    public class RobotEntityTypeConfiguration : IEntityTypeConfiguration<Robot>
    {
        public void Configure(EntityTypeBuilder<Robot> builder)
        {
            Guards.ThrowIfNull(builder);

            builder.ToTable("Robots");
            builder.Property(x => x.Name).IsRequired();            
            builder.OwnsOne(x => x.Coordinates, y =>
            {
                y.Property(z => z!.XPos).HasColumnName("xPos");
                y.Property(z => z!.YPos).HasColumnName("yPos");
            });            
        }        
    }
}
