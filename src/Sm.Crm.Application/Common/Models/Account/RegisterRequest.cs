using System.ComponentModel.DataAnnotations;

namespace Sm.Crm.Application.Common.Models.Account;

public class RegisterRequest
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(6)]
    public string? UserName { get; set; }

    [Required]
    [MinLength(6)]
    public string? Password { get; set; }

    [Required]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    [Required]
    public bool ConfirmAggreement { get; set; }
    
    public bool IsExternalAuthentication { get; set; }
}