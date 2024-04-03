namespace Sm.Crm.Domain.Common;

public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}