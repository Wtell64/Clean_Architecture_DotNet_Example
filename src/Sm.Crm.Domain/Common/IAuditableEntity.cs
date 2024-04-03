namespace Sm.Crm.Domain.Common;

public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; set; }

    string? CreatedBy { get; set; }

    DateTimeOffset? LastModifiedAt { get; set; }

    string? LastModifiedBy { get; set; }

    DateTimeOffset? DeletedAt { get; set; }

    string? DeletedBy { get; set; }
}