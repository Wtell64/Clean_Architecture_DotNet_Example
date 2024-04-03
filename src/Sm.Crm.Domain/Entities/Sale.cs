using Sm.Crm.Domain.Common;

namespace Sm.Crm.Domain.Entities;

public class Sale : BaseEntity
{
    public int RequestId { get; set; }
    public string EmployeeUserId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal SaleAmount { get; set; }
    public string Description { get; set; }

    // Navigation Properties
    public Request RequestFk { get; set; }

    //public User EmployeeUserFk { get; set; }
}