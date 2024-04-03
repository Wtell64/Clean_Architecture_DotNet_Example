using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Infrastructure.Persistence.Configurations;

public class TaskStatusConfiguration : IEntityTypeConfiguration<TaskStatusItem>
{
    public new void Configure(EntityTypeBuilder<TaskStatusItem> builder)
    {
        builder.ToTable("TaskStatus", "LST");

        builder.Property(b => b.Name).HasMaxLength(150);
    }
}