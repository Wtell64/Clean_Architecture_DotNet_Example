using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(nameof(Employee), "dbo");

        builder
            .Property(b => b.IdentityNumber)
            .HasMaxLength(11);
    }
}