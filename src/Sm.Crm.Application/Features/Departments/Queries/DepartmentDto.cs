using AutoMapper;
using Sm.Crm.Application.Features.Departments.Commands.CreateDepartment;
using Sm.Crm.Application.Features.Departments.Commands.UpdateDepartment;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Departments.Queries;

public class DepartmentDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, CreateDepartmentCommand>().ReverseMap();
            CreateMap<Department, UpdateDepartmentCommand>().ReverseMap();
        }
    }
}