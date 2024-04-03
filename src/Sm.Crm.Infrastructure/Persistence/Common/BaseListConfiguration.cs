using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Infrastructure.Persistence.Common;

public class BaseListConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseListEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(typeof(TEntity).Name, "LST");

        builder.Property(b => b.Name).HasMaxLength(150);
    }
}