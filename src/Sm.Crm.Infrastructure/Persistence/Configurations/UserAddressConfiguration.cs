using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Infrastructure.Persistence.Configurations;

public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.ToTable(nameof(UserAddress), "dbo");

        builder.Property(b => b.Address).HasMaxLength(150);
        builder.Property(b => b.Country).HasMaxLength(100);
        builder.Property(b => b.City).HasMaxLength(50);
    }
}