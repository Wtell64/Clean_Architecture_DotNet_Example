using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Domain.Entities;

public class UserAddress : BaseEntity
{
    public Guid UserId { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public AddressTypeEnum? AddressType { get; set; }

    public User UserFk { get; set; }
}