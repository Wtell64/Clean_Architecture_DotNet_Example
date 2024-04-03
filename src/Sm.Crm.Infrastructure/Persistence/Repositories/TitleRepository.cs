using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Repositories;

public class TitleRepository : BaseRepository<Title, int>, ITitleRepository
{
    public TitleRepository(ApplicationDbContext context) : base(context)
    {
    }
}