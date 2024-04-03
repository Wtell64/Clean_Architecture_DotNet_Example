using Microsoft.AspNetCore.Identity;

namespace Sm.Crm.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ActivationCode { get; set; }
    public string? RefreshToken { get; set; }
}