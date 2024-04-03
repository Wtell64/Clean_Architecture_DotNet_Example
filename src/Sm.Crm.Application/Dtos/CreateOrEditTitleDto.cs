using AutoMapper;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Dtos;

public class CreateOrEditTitleDto
{
    public int? Id { get; set; }
    public string Name { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Title, CreateOrEditTitleDto>().ReverseMap();
        }
    }
}