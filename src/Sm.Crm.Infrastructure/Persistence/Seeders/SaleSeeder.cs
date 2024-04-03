using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class SaleSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Sales.Any()) return;

        var userIds = context.Users.Select(x => x.Id).ToList();
        var requestIds = context.Requests.Select(x => x.Id).ToList();

        var faker = new Faker<Sale>()
            .RuleFor(e => e.Description, c => c.Random.Words(10))
            .RuleFor(e => e.SaleAmount, c => c.Random.Decimal(500, 100000))
            .RuleFor(e => e.SaleDate, c => c.Date.Past())
            .RuleFor(e => e.RequestId, c => c.PickRandom(requestIds));
        //.RuleFor(e => e.EmployeeUserId, c => c.PickRandom(userIds));

        var list = faker.Generate(500);

        await context.Sales.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}