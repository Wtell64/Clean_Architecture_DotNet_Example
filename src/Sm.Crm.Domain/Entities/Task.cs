using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Domain.Entities;

public class TaskItem : BaseEntity
{
    public int RequestId { get; set; }
    public int EmployeeUserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
    public int TaskStatusId { get; set; }

    #region Navigation Properties

    public Request RequestFk { get; set; }
    public Employee EmployeeUserFk { get; set; }
    public TaskStatusItem TaskStatusFk { get; set; }

    #endregion
}