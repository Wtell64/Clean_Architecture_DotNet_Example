using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), "USR");

        builder.Property(b => b.Username).HasMaxLength(30).IsRequired();
        builder.Property(b => b.Email).HasMaxLength(150).IsRequired();
        builder.Property(b => b.Password).HasMaxLength(150).IsRequired();
    }
}