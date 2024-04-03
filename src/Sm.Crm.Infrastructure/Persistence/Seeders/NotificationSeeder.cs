using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Infrastructure.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;
internal class NotificationSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Notificiations.Any()) return;
        List<Guid> userIds = context.Users.Select(x => x.Id).ToList();
        Faker<Notification> faker = new Faker<Notification>()
            .RuleFor(a => a.IsRead, f => f.PickRandom(true, false))
            .RuleFor(a => a.CreatedAt, f => f.Date.Between(new DateTime(2023, 1, 1), new DateTime(2024, 12, 31)))
            .RuleFor(a => a.CreatedBy, f => f.PickRandom(userIds))
            .RuleFor(a => a.Title, f => f.Lorem.Sentence(15))
            .RuleFor(a => a.Description, f => f.Lorem.Sentences())
            .RuleFor(a => a.UserId, f => f.PickRandom(userIds));
            
        var list = faker.Generate(100);
        await context.Notificiations.AddRangeAsync(list);
        await context.SaveChangesAsync();


    }
}
