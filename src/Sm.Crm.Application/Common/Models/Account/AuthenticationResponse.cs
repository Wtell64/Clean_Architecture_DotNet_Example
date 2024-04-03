namespace Sm.Crm.Application.Common.Models.Account;

public class AuthenticationResponse
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<string> Roles { get; set; } = [];
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}