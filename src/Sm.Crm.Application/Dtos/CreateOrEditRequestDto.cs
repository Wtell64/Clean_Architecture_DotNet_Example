using AutoMapper;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Dtos;

public class CreateOrEditRequestDto
{
    public int? Id { get; set; }
    public Guid CustomerUserId { get; set; }
    public Guid EmployeeUserId { get; set; }
    public int RequestStatusId { get; set; }
    public string? Description { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Request, CreateOrEditRequestDto>().ReverseMap();
        }
    }
}