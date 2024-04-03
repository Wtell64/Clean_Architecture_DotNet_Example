using Bogus;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class UserAddressSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.UserAddresses.Any()) return;

        var userIds = await context.Users.Select(x => x.Id).ToListAsync();
        
        var trSet = new Bogus.DataSets.Name(locale: "tr");
        var faker = new Faker<UserAddress>()
            .RuleFor(u => u.UserId, u => u.PickRandom(userIds))
            .RuleFor(u => u.Address, u => u.Address.FullAddress())
            .RuleFor(u => u.Country, u => u.Address.Country())
            .RuleFor(u => u.City, u => u.Address.City())
            .RuleFor(u => u.AddressType, u => u.PickRandom<AddressTypeEnum>());
        var list = faker.Generate(100);
        await context.UserAddresses.AddRangeAsync(list);
        
        await context.SaveChangesAsync();
    }
}