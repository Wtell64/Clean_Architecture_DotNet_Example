using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

internal class TaskSeeder : ISeeder
{
    public async Task Seed(IApplicationDbContext context)
    {
        if (context.Tasks.Any()) return;

        var trSet = new Bogus.DataSets.Company(locale: "tr");

        var taskStatusIds = context.TaskStatuses.Select(t => t.Id).ToList();
        var requestIds = context.Requests.Select(t => t.Id).ToList();
        var employeeUserIds = context.Employees.Select(t => t.Id).ToList();

        var faker = new Faker<TaskItem>()
        .RuleFor(t => t.Description, c => c.Lorem.Sentence())
        .RuleFor(t => t.StartDate, c => c.Date.Past())
        .RuleFor(t => t.EndDate, c => DateTime.Now)
        .RuleFor(t => t.TaskStatusId, c => c.PickRandom(taskStatusIds))
        .RuleFor(t => t.RequestId, c => c.PickRandom(requestIds))
        .RuleFor(t => t.EmployeeUserId, c => c.PickRandom(employeeUserIds));

        var list = faker.Generate(500);

        await context.Tasks.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}