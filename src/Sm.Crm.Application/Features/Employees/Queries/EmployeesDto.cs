using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.Employees.Queries;

public class EmployeesDto
{
	public int Id { get; set; }
	public Guid UserId { get; set; }
	public User? User { get; set; }
	public string Name { get; set; }
	public string LastName { get; set; }
	public string FullName => $"{Name} {LastName}";
	public string Email { get; set; }
	public string IdentityNumber { get; set; }
	public int? DepartmentId { get; set; }
	public Department? Department { get; set; }
	public string? DepartmentName { get; set; }
	public DateTime? StartDate { get; set; }
	public int? StatusTypeId { get; set; }
	public StatusType? StatusType { get; set; }
	public string? StatusTypeName { get; set; }
	public int? TerritoryId { get; set; }
	public Territory? Territory { get; set; }
	public string? TerritoryName { get; set; }
	public DateOnly? BirthDate { get; set; }
	public Guid? ReportsToUserId { get; set; }
	public string? PhotoPath { get; set; }
}