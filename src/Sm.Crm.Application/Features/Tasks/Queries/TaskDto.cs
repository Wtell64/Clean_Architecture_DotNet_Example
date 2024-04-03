namespace Sm.Crm.Application.Features.Tasks.Queries;

public class TaskDto
{
    public int? Id { get; set; }
    public int? RequestId { get; set; }
    public int? EmployeeUserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public int? TaskStatusId { get; set; }

}
