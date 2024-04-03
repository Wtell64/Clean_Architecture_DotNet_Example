using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models.Account;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;
using System.Security.Claims;

namespace Sm.Crm.Infrastructure.Authentication;

public class CookieAccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieAccountService(IUserRepository userRepository, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthenticationResponse?> AuthenticateAsync(AuthenticationRequest request)
    {
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(e => e.Email == request.Email.Trim());
        if (user == null) return null;

        if (!AccountHelper.HashValidate(user.Password, request.Password.Trim())) return null;

        // ClaimsIdentity içerisindeki bilgiler (Kimlik'te yazan bilgiler)
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Email, request.Email)
        };

        if (user.Roles.Any())
        {
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
        }

        // ClaimsIdentity (Kimlik)
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Principle (Cüzdan)
        var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

        var authProperties = new AuthenticationProperties()
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.Now.AddDays(1)
        };

        if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
        {
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrinciple,
                authProperties
            );
        }

        return new AuthenticationResponse
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            UserName = user.Username,
            Roles = user.Roles.Split(',').ToList(),
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public async Task LogoutAsync()
    {
        if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(e => e.Id.ToString() == userId);
        return user?.Username;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        return await _userRepository.GetAll().AnyAsync(e => e.Id.ToString() == userId && e.Roles.Contains(role));
    }

    public async Task<AuthenticationResponse?> RegisterAsync(RegisterRequest request)
    {
        var existUser = await _userRepository.GetAll().FirstOrDefaultAsync(e => e.Email == request.Email.Trim());
        if (existUser != null) return null;

        var user = new User
        {
            Password = AccountHelper.HashCreate(request.Password)
        };
        await _userRepository.Create(user);

        var body = @"
            <h1>SM-CRM</h1>
            <p>User created! Please activate your account.</p>";
        await _emailService.SendEmailAsync(user.Email, "CRM Register", body);

        return new AuthenticationResponse
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            UserName = user.Username,
            Roles = user.Roles.Split(',').ToList(),
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public Task<bool> ActivateUserAsync(string userId, string code)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsUserExist(string email)
    {
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == email);
        return user != null;
    }
}