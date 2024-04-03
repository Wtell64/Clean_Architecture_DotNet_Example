using System.ComponentModel.DataAnnotations;

namespace Sm.Crm.Application.Common.Models.Account;

public class AuthenticationRequest
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    public bool RememberMe { get; set; } = true;
    public bool IsExternalAuthentication { get; set; }
}