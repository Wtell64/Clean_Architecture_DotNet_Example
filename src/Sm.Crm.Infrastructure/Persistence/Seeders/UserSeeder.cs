using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Infrastructure.Authentication;
using Sm.Crm.Infrastructure.Persistence.Common;
using static Bogus.DataSets.Name;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class UserSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Users.Any()) return;

        var adminUser = new User()
        {
            Username = "admin",
            Password = AccountHelper.HashCreate("123qwe"),
            Email = "admin@localhost",
            Roles = "Administrator",
            Gender = GenderEnum.Male,
            FirstName = "Admin",
            LastName = "Admin",
            IsActive = true
        };
        await context.Users.AddAsync(adminUser);

        var trSet = new Bogus.DataSets.Name(locale: "tr");
        var faker = new Faker<User>()
            .RuleFor(u => u.Username, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.Password, AccountHelper.HashCreate("123qwe"))
            .RuleFor(u => u.Gender, f => f.PickRandom<GenderEnum>())
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName((Gender)u.Gender))
            .RuleFor(u => u.Roles, "Customer")
            .RuleFor(u => u.IsActive, true);
        var list = faker.Generate(100);
        await context.Users.AddRangeAsync(list);

        await context.SaveChangesAsync();
    }
}