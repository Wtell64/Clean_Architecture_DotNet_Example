using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class StatusTypeSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.StatusTypes.Any()) return;

        var list = new List<StatusType>()
        {
            new() { Name = "Started" },
            new() { Name = "OnHold" },
            new() { Name = "InProgress" },
            new() { Name = "Completed" },
            new() { Name = "Cancelled" }
        };
        await context.StatusTypes.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}