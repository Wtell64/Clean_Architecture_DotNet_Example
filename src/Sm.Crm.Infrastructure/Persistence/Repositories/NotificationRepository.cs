using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Infrastructure.Persistence.Repositories;
public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    private readonly ApplicationDbContext _context;


    public NotificationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}
