using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Infrastructure.Persistence.Configurations;

public class RegionConfigration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable(nameof(Region), "LST");

        builder
            .Property(b => b.Name)
            .HasMaxLength(100);
    }
}
