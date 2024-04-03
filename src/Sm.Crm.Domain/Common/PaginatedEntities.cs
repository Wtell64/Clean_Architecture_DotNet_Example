namespace Sm.Crm.Domain.Common;

public class PaginatedEntities<TEntity>
{
    public int Count { get; set; }

    public IEnumerable<TEntity> Entities { get; set; }
}