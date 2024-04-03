using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Domain.Entities;

public class Request : BaseEntity
{
    public long? CustomerId { get; set; }
    public int? EmployeeId { get; set; }
    public int? RequestStatusId { get; set; }
    public string? Description { get; set; }

    // Navigation Properties
    public Customer? CustomerFk { get; set; }
    public Employee? EmployeeFk { get; set; }
    public RequestStatus? RequestStatusFk { get; set; }
}

// Customer (Özge) -> Employee (Cem) -> Request (10 adet Office lisans)