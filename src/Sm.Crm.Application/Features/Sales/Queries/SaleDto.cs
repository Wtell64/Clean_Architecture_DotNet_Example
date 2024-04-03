namespace Sm.Crm.Application.Features.Sales.Queries;

public class SaleDto
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public string EmployeeUserId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal SaleAmount { get; set; }
    public string Description { get; set; }
}