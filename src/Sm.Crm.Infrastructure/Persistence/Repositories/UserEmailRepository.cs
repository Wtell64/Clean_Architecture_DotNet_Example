using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Repositories;

public class UserEmailRepository : BaseRepository<UserEmail>, IUserEmailRepository
{
    public UserEmailRepository(ApplicationDbContext context) : base(context)
    {
    }
}