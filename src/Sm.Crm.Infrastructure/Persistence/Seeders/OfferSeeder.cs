using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class OfferSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Offers.Any()) return;

        var offerFaker = new Bogus.DataSets.Name(locale: "tr");

         var userIds = context.Users.Select(x => x.Id).ToList();
         var requestIds = context.Requests.Select(x => x.Id).ToList();

        var faker = new Faker<Offer>()
        .RuleFor(o => o.OfferDate, f => f.Date.Between(new DateTime(2000, 1, 1), new DateTime(2024, 12, 31)))
        .RuleFor(o => o.BidAmount, f => f.Random.Decimal(1, 9000000))
        .RuleFor(o => o.EmployeeUserId, f => f.PickRandom(userIds))
        .RuleFor(o => o.RequestId, f => f.PickRandom(requestIds))
        .RuleFor(o => o.OfferStatusId, f => f.Random.Int(1, 999));

        faker.Generate(500);

        await context.Offers.AddAsync(faker);
        await context.SaveChangesAsync();
    }
}