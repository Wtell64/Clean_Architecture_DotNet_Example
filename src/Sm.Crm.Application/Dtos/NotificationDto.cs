using AutoMapper;
using Sm.Crm.Domain.Entities;


namespace Sm.Crm.Application.Dtos;
public class NotificationDto
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
            CreateMap<Notification, NotificationDto>().ReverseMap();
        }
    }
}
