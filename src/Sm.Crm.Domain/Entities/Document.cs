using Sm.Crm.Domain.Common;

namespace Sm.Crm.Domain.Entities;

public class Document : BaseAuditableEntity
{
    public string DocumentFileName { get; set; }

    public Guid UserId { get; set; }
    public int RequestId { get; set; }
    public int DocumentTypeId { get; set; }
}