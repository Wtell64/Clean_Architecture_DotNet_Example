using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Repositories;

public class UserAddressRepository : BaseRepository<UserAddress>, IUserAddressRepository
{
    public UserAddressRepository(ApplicationDbContext context) : base(context)
    {
    }
}