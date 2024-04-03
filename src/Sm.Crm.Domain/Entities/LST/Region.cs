using Sm.Crm.Domain.Common;

namespace Sm.Crm.Domain.Entities.LST;

public class Region : BaseListEntity
{
    public int? ParentId { get; set; }
}