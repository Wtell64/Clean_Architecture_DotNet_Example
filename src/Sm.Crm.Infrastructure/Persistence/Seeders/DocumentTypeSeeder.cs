using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class DocumentTypeSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.DocumentTypes.Any()) return;
        var list = new List<DocumentType>()
        {
            new() { Name = "Invoice" },
            new() { Name = "Contract" },
            new() { Name = "Report" },
            new() { Name = "Receip" },
        };
        await context.DocumentTypes.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}