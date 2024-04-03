using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Repositories;

public class StatusTypeRepository : BaseRepository<StatusType>, IStatusTypeRepository
{
    public StatusTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}