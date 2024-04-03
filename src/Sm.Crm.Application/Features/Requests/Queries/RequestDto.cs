using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Sm.Crm.Application.Features.Requests.Queries;
public class RequestDto
{
    public int Id { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerFirstName { get; set; }
    public string? CustomerLastName { get; set; }
    public string? CustomerFullName => CustomerFirstName + " " + CustomerLastName;
    public string CompanyName { get; set; }
    public int? EmployeeId { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }
    public string? EmployeeFullName => EmployeeFirstName + " " + EmployeeLastName;
    public string DepartmentName { get; set; }
    public int RequestStatusId { get; set; }
    public string? RequestStatusName { get; set; }
    public string? Description { get; set; }
}