using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class DocumentSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Documents.Any()) return;

        var userIds = context.Users.Select(x => x.Id).ToList();
        var requestIds = context.Requests.Select(x => x.Id).ToList();
        var documentTypeIds = context.DocumentTypes.Select(x => x.Id).ToList();

        var faker = new Faker<Document>()
            .RuleFor(d => d.DocumentFileName, c => c.Image.PicsumUrl())
            .RuleFor(d => d.UserId, c => c.PickRandom(userIds))
            .RuleFor(d => d.RequestId, c => c.PickRandom(requestIds))
            .RuleFor(d => d.DocumentTypeId, c => c.PickRandom(documentTypeIds));

        var list = faker.Generate(20);
        await context.Documents.AddRangeAsync(list);
    }
}