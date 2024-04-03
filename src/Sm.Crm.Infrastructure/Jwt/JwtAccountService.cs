using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models.Account;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Infrastructure.Identity;
using System.Security.Claims;

namespace Sm.Crm.Infrastructure.Jwt;
public class JwtAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public JwtAccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<List<Claim>?> AuthenticateAsync(AuthenticationRequest request)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == request.Email.Trim());
        if (user == null) return null;

        var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password.Trim(), false);
        if (!checkPassword.Succeeded) return null;

        return await GetUserClaims(user);
    }

    private async Task<List<Claim>?> GetUserClaims(ApplicationUser? user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.FirstName ?? ""),
            new Claim(ClaimTypes.Surname, user.LastName ?? ""),
            new Claim(ClaimTypes.Email, user.Email)
        };

        if (roles.Any())
        {
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
        }

        return claims;
    }

    public async Task<List<Claim>?> AuthenticateByUserIdAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == id);
        if (user == null) return null;

        return await GetUserClaims(user);
    }

    public async Task<RefreshTokenResponse?> GetUserByRefreshToken(string refreshToken)
    {
        var user = await _userManager.Users.Where(e => e.RefreshToken == refreshToken).FirstOrDefaultAsync();
        if (user == null) return null;

        return new RefreshTokenResponse
        {
            Id = user.Id,
            RefreshToken = user.RefreshToken
        };
    }

    public async Task<bool> UpdateRefreshToken(string userId, string refreshToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == userId);
        if (user == null) return false;

        user.RefreshToken = refreshToken;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
}
