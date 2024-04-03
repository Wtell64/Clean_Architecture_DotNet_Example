using AutoMapper;
using Sm.Crm.Application.Features.Tasks.Queries;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Tasks;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TaskItem, TaskDto>()
        .ReverseMap();
    }
}
