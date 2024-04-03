using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Infrastructure.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable(nameof(TaskItem), "dbo");

        builder.Property(t => t.TaskStatusId).IsRequired();
        builder.Property(t => t.Description).IsRequired();
        builder.Property(t => t.RequestId).IsRequired();
        builder.Property(t => t.EmployeeUserId).IsRequired();
    }
}