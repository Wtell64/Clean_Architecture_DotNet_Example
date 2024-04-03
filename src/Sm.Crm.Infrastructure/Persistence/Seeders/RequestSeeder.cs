using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class RequestSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Requests.Any()) return;

        var customerIds = context.Customers.Select(x => x.Id).ToList();
        var employeeIds = context.Employees.Select(x => x.Id).ToList();
        var requestStatusIds = context.RequestStatuses.Select(x => x.Id).ToList();

        var faker = new Faker<Request>()
            .RuleFor(r => r.CustomerId, f => f.PickRandom(customerIds))
            .RuleFor(r => r.EmployeeId, f => f.PickRandom(employeeIds))
            .RuleFor(r => r.RequestStatusId, f => f.PickRandom(requestStatusIds))
            .RuleFor(r => r.Description, f => f.Lorem.Sentence())
        ;

        var requests = faker.Generate(100);
        await context.Requests.AddRangeAsync(requests);
        await context.SaveChangesAsync();
    }
}