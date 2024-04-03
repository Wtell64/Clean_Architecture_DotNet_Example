using Sm.Crm.Application.Common.Models.Account;

namespace Sm.Crm.Application.Common.Interfaces;

public interface IAccountService
{
    Task<AuthenticationResponse?> AuthenticateAsync(AuthenticationRequest request);

    Task LogoutAsync();

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<AuthenticationResponse?> RegisterAsync(RegisterRequest request);

    Task<string?> GetUserNameAsync(string userId);

    Task<bool> ActivateUserAsync(string userId, string code);

    Task<bool> IsUserExist(string email);
}