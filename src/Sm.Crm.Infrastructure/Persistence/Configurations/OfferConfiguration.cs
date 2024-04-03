using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Infrastructure.Persistence.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable(nameof(Offer), "dbo");

        builder.Property(a => a.RequestId).IsRequired();

        builder.Property(a => a.EmployeeUserId).IsRequired();

        builder.Property(a => a.BidAmount).IsRequired();

        builder.Property(a => a.OfferStatusId).IsRequired();

        builder.Property(a => a.OfferDate).IsRequired();
    }
}