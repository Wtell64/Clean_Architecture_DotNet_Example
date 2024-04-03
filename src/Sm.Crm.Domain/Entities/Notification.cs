using Sm.Crm.Domain.Common;

namespace Sm.Crm.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string Description { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    //public User User { get; set; } = null!;
}