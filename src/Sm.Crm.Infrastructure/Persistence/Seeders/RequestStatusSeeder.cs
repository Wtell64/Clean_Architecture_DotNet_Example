using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class RequestStatusSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.RequestStatuses.Any()) return;

        var list = new List<RequestStatus>()
        {
            new() { Name = "Success" },
            new() { Name = "Error " },
            new() { Name = "Processing" },
            new() { Name = "Pending" },
            new() { Name = "Cancelled" }
        };
        await context.RequestStatuses.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}