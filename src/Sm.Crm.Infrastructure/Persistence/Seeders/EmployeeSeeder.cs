using Bogus;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Seeders;

public class EmployeeSeeder : ISeeder
{
	public async Task Seed(IApplicationDbContext context)
	{
		if (context.Employees.Any())
			return;

		var userIds = context.Users.Select(x => x.Id).ToList();
		var departmentIds = context.Departments.Select(x => x.Id).ToList();
		var satusTypeIds = context.StatusTypes.Select(x => x.Id).ToList();
		//var territoryIds = context.Territories.Select(x => x.Id).ToList();

		var faker = new Faker<Employee>()
			.RuleFor(e => e.UserId, c => c.PickRandom(userIds))
			.RuleFor(e => e.IdentityNumber, c => c.Random.Long(11111111111, 59999999999).ToString())

			.RuleFor(e => e.DepartmentId, c => c.PickRandom(departmentIds))
			.RuleFor(e => e.StartDate, c => new DateTime(c.Random.Int(1980, 2000), 1, 1))

			//.RuleFor(e => e.StatusTypeId, c => c.PickRandom(satusTypeIds))
			//.RuleFor(e => e.TerritoryId, c => c.PickRandom(territoryIds))

			.RuleFor(e => e.BirthDate, c => new DateOnly(c.Random.Int(1980, 2000), 1, 1))
			.RuleFor(e => e.ReportsToUserId, c => c.PickRandom(userIds));

		var list = faker.Generate(100);
		await context.Employees.AddRangeAsync(list);
		await context.SaveChangesAsync();
	}
}