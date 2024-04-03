using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Domain.Entities;

public class Employee : BaseEntity
{
	public Guid UserId { get; set; }
	public string IdentityNumber { get; set; } = null!;
	public int? DepartmentId { get; set; }
	public DateTime? StartDate { get; set; }
	public int? StatusTypeId { get; set; }
	public int? TerritoryId { get; set; }
	public DateOnly? BirthDate { get; set; }
	public Guid? ReportsToUserId { get; set; }
	public string? PhotoPath { get; set; }

	public User? UserFk { get; set; }
	public Department? DepartmentFk { get; set; }
	public Territory? TerritoryFk { get; set; }
	public StatusType? StatusTypeFk { get; set; }
}