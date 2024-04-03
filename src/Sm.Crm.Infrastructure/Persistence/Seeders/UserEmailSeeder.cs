using Bogus;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class UserEmailSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.UserEmails.Any()) return;

        var userIds = await context.Users.Select(x => x.Id).ToListAsync();
        
        var trSet = new Bogus.DataSets.Name(locale: "tr");
        var faker = new Faker<UserEmail>()
            
            .RuleFor(u => u.EmailAddress, u => u.Internet.Email())
           
            .RuleFor(u => u.EmailType, u => u.PickRandom<EmailTypeEnum>());
        var list = faker.Generate(100);
        await context.UserEmails.AddRangeAsync(list);
        
        await context.SaveChangesAsync();
    }
}