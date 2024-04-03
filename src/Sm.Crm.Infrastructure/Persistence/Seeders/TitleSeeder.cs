using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class TitleSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Titles.Any()) return;

        var trSet = new Bogus.DataSets.Name(locale: "tr");
        var faker = new Faker<Title>()
            .RuleFor(e => e.Name, c => trSet.JobTitle());

        var list = faker.Generate(40);
        await context.Titles.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}