using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sm.Crm.Application.Common.Models.Account;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Infrastructure.Authentication;
using Sm.Crm.Infrastructure.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sm.Crm.WebApi.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/api/auth")
            .WithTags("Auth"); // Swagger içerisinde ayrı bir kategori olarak göstermeyi sağlar.

        group.MapPost("/authenticate", Authenticate);
        group.MapPost("/refresh-token", AuthenticateByRefreshToken);

        return group;
    }

    private static async Task<IResult> Authenticate(AuthenticationRequest request, JwtAccountService accountService, IConfiguration configuration)
    {
        if (String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.Password))
            return Results.BadRequest("Invalid user");

        var authenticatedUserClaims = await accountService.AuthenticateAsync(request);
        if (authenticatedUserClaims == null) return Results.Unauthorized();

        var token = await GetJwtToken(authenticatedUserClaims, accountService, configuration);

        return token;
    }

    private static async Task<IResult> AuthenticateByRefreshToken([FromBody] string refreshToken, JwtAccountService accountService, IConfiguration configuration)
    {
        if (refreshToken == null) return Results.BadRequest();

        var refreshTokenUser = await accountService.GetUserByRefreshToken(refreshToken);
        if (refreshTokenUser == null) return Results.Unauthorized();

        if (refreshTokenUser.RefreshToken == null || refreshTokenUser.RefreshToken != refreshToken)
            return Results.Unauthorized();

        var authenticatedUserClaims = await accountService.AuthenticateByUserIdAsync(refreshTokenUser.Id);
        if (authenticatedUserClaims == null) return Results.Unauthorized();

        var token = await GetJwtToken(authenticatedUserClaims, accountService, configuration);

        return token;
    }

    private static async Task<IResult> GetJwtToken(List<Claim> authenticatedUserClaims, JwtAccountService accountService, IConfiguration configuration)
    {
        var expireInMinute = Convert.ToDouble(configuration["Authentication:Jwt:ExpireTimeInMinute"]);
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:SigningKey"]));
        var tokenOptions = new JwtSecurityToken(
            issuer: configuration["Authentication:Jwt:Issuer"],
            audience: configuration["Authentication:Jwt:Audience"],
            claims: authenticatedUserClaims,
            expires: DateTime.UtcNow.AddMinutes(expireInMinute),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        var refreshToken = AccountHelper.GenerateSalt();

        var userId = authenticatedUserClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        await accountService.UpdateRefreshToken(userId.Value, Convert.ToBase64String(refreshToken));

        return Results.Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = TimeSpan.FromMinutes(expireInMinute).TotalSeconds,
        });
    }
}