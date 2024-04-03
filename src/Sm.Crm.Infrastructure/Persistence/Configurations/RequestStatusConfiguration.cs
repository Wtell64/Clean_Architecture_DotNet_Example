using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Infrastructure.Persistence.Configurations;

public class RequestStatusConfiguration : IEntityTypeConfiguration<RequestStatus>
{
    public void Configure(EntityTypeBuilder<RequestStatus> builder)
    {
        builder.ToTable(nameof(RequestStatus), "LST");

        builder
            .Property(b => b.Name)
            .HasMaxLength(150);
    }
}