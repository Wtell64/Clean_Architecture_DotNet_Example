using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Repositories;

public class RequestStatusRepository : BaseRepository<RequestStatus>, IRequestStatusRepository
{
    public RequestStatusRepository(ApplicationDbContext context) : base(context)
    {
    }
}