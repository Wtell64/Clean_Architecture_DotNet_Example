using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Roles { get; set; }
    public GenderEnum? Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ActivationKey { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public Customer CustomerFk { get; set; }
}