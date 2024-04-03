using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Constants;
using Sm.Crm.Infrastructure.Identity;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class IdentitySeeder : ISeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public IdentitySeeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Seed(IApplicationDbContext context)
    {
        if (await _userManager.Users.AnyAsync()) return;

        // Default roles
        var administratorRole = new ApplicationRole(Roles.Administrator);
        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }
        var customerRole = new ApplicationRole(Roles.Customer);
        if (_roleManager.Roles.All(r => r.Name != customerRole.Name))
        {
            await _roleManager.CreateAsync(customerRole);
        }

        // Default users
        var administrator = new ApplicationUser
        {
            UserName = "administrator@localhost",
            Email = "administrator@localhost",
            FirstName = "Admin",
            LastName = "Admin",
            EmailConfirmed = true
        };
        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        var trSet = new Bogus.DataSets.Name(locale: "tr");

        var faker = new Faker<ApplicationUser>()
            .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
            .RuleFor(u => u.EmailConfirmed, true);
        var list = faker.Generate(100);
        foreach (var user in list)
        {
            await _userManager.CreateAsync(user, "Customer1!");
            await _userManager.AddToRolesAsync(user, new[] { customerRole.Name });
        }
    }
}