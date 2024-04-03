using AutoMapper;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Dtos;

public class CreateOrUpdateEmployeeDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string IdentityNumber { get; set; }
    public int? DepartmentId { get; set; }
    public DateTime? StartDate { get; set; }
    public int? StatusTypeId { get; set; }
    public int? TerritoryId { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Guid? ReportsToUserId { get; set; }
    public string? PhotoPath { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Employee, CreateOrUpdateEmployeeDto>().ReverseMap();
        }
    }
}