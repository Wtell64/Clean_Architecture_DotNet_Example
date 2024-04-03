using Microsoft.AspNetCore.Identity;

namespace Sm.Crm.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}