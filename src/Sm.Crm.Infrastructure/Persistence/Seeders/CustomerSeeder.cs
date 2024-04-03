using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class CustomerSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Customers.Any()) return;

        var trSet = new Bogus.DataSets.Company(locale: "tr");

        var faker = new Faker<Customer>()
            .RuleFor(e => e.CompanyName, c => trSet.CompanyName())
            .RuleFor(e => e.IdentityNumber, c => c.Random.Long(11111111111, 59999999999).ToString())
            .RuleFor(e => e.BirthDate, c => new DateOnly(c.Random.Int(1980, 2000), 1, 1))
            .RuleFor(u => u.CustomerType, f => f.PickRandom<CustomerTypeEnum>())
        ;
        //.RuleFor(e => e.UserId, c => c.Random.Guid(1, 1000))
        //StatusTypeId = 1,
        //TerritoryId = 1,
        //TitleId = 1,

        var list = faker.Generate(500);

        await context.Customers.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}