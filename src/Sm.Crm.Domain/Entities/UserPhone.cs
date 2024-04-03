using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Domain.Entities;

public class UserPhone : BaseEntity
{
    public int UserId { get; set; }

    public string PhoneNumber { get; set; }

    public PhoneTypeEnum PhoneType { get; set; }

    // Navigation Properties
    public User UserFk { get; set; } = null!;
}