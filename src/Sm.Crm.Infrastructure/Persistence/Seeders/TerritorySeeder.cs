using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class TerritorySeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Territories.Any()) return;

        var list = new List<Territory>()
        {
            new() { Name = "İstanbul-Avrupa" },
            new() { Name = "İstanbul-Asya" },
            new() { Name = "İç Anadolu" }
        };
        await context.Territories.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}