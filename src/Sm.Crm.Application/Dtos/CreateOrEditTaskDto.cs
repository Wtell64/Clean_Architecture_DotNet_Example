using AutoMapper;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Dtos;

public class CreateOrEditTaskDto
{
    public int? Id { get; set; }
    public int? RequestId { get; set; }
    public int? EmployeeUserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public int? TaskStatusId { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TaskItem, CreateOrEditTaskDto>()
            .ReverseMap();
        }
    }
}