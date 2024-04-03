using AutoMapper;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Dtos;

public class StatusTypeDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StatusType, StatusTypeDto>().ReverseMap();
        }
    }
}