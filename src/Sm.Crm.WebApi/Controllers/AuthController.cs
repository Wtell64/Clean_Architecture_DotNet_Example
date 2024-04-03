using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sm.Crm.Application.Common.Models.Account;
using Sm.Crm.Infrastructure.Authentication;
using Sm.Crm.Infrastructure.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Sm.Crm.WebApi.Controllers;

public class Auth2Controller : FeatureController
{
    private readonly JwtAccountService _accountService;
    private readonly IConfiguration _configuration;

    public Auth2Controller(JwtAccountService accountService, IConfiguration configuration)
    {
        _accountService = accountService;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticationRequest request)
    {
        var authenticatedUserClaims = await _accountService.AuthenticateAsync(request);
        if (authenticatedUserClaims == null) return Unauthorized();

        var expireInMinute = Convert.ToDouble(_configuration["Authentication:Jwt:ExpireTimeInMinute"]);
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Jwt:SigningKey"]));
        var tokenOptions = new JwtSecurityToken(
            issuer: _configuration["Authentication:Jwt:Issuer"],
            audience: _configuration["Authentication:Jwt:Audience"],
            claims: authenticatedUserClaims,
            expires: DateTime.UtcNow.AddMinutes(expireInMinute),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        var refreshToken = AccountHelper.GenerateSalt();

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = TimeSpan.FromMinutes(expireInMinute).TotalSeconds,
        });
    }
}
