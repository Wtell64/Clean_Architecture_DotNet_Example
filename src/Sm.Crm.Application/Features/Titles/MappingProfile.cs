using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Features.Titles.Commands.CreateTitle;
using Sm.Crm.Application.Features.Titles.Commands.UpdateTitle;
using Sm.Crm.Application.Features.Titles.Queries;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.Titles;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Title, CreateTitleCommand>().ReverseMap();
        CreateMap<Title, UpdateTitleCommand>().ReverseMap();
        CreateMap<Title, TitleDto>();
        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}