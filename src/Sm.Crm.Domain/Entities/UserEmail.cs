using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Domain.Entities;

public class UserEmail : BaseEntity
{
    public int? UserId { get; set; }
    public string? EmailAddress { get; set; }
    public EmailTypeEnum EmailType { get; set; }

    // Navigation Properties
    public User? UserFk { get; set; }
}