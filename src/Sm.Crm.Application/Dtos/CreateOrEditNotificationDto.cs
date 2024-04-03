using AutoMapper;
using Sm.Crm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Dtos;
public class CreateOrEditNotificationDto
{
    public long? Id { get; set; }

    public Guid? UserId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; } = null!;
    public bool? IsRead { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Notification, CreateOrEditNotificationDto>().ReverseMap();
        }
    }
}

