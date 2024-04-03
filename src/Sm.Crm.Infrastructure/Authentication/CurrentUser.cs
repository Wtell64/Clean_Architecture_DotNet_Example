using Microsoft.AspNetCore.Http;
using Sm.Crm.Application.Common.Interfaces;
using System.Security.Claims;

namespace Sm.Crm.Infrastructure.Authentication;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}