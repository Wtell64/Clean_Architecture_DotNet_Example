using AutoMapper;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Dtos;

public class CreateOrUpdateDepartmentDto
{
    public int? Id { get; set; }
    public string Name { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Department, CreateOrUpdateDepartmentDto>().ReverseMap();
        }
    }
}