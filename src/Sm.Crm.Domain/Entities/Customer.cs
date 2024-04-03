using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Domain.Entities;

public class Customer : BaseAuditableEntity<long>
{
    public string? IdentityNumber { get; set; }
    public CustomerTypeEnum? CustomerType { get; set; }
    public string? CompanyName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Guid? UserId { get; set; }
    public int? StatusTypeId { get; set; }
    public int? TitleId { get; set; }
    public int? TerritoryId { get; set; }

    #region Navigation Properties

    public User? UserFk { get; set; }
    public StatusType? StatusTypeFk { get; set; }
    public Territory? TerritoryFk { get; set; }
    public Title? TitleFk { get; set; }

    #endregion
}