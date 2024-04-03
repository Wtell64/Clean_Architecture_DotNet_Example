using AutoMapper;
using Sm.Crm.Application.Features.Employees.Queries;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Employees;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Employee, EmployeesDto>()
			.ForMember(e => e.Name, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.FirstName : "-"))
			.ForMember(e => e.LastName, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.LastName : "-"))
			.ForMember(e => e.Email, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.Email : "-"))
			.ForMember(e => e.DepartmentName, m => m.MapFrom(d => d.DepartmentFk != null ? d.DepartmentFk.Name : "-"))
			.ForMember(e => e.StatusTypeName, m => m.MapFrom(d => d.StatusTypeFk != null ? d.StatusTypeFk.Name : "-"))
			.ForMember(e => e.TerritoryName, m => m.MapFrom(d => d.TerritoryFk != null ? d.TerritoryFk.Name : "-"))
			.ReverseMap();
	}
}