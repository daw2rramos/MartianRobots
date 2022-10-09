using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartianRobots.Infrastructure.Configurations
{
    public class MapEntityTypeConfiguration : IEntityTypeConfiguration<Map>
    {
        public void Configure(EntityTypeBuilder<Map> builder)
        {
            Guards.ThrowIfNull(builder);

            builder.Property(x => x.Name).IsRequired();
            //builder.OwnsOne(x => x.Origin, y =>
            //{
            //    y.Property(z => z!.XPos).HasColumnName("xOrigin").HasDefaultValue(0);
            //    y.Property(z => z!.YPos).HasColumnName("yOrigin").HasDefaultValue(0);
            //});
            //builder.OwnsOne(x => x.TopRight, y =>
            //{
            //    y.Property(z => z!.XPos).HasColumnName("xBound");
            //    y.Property(z => z!.YPos).HasColumnName("yBound");
            //});                      
        }        
    }
}
