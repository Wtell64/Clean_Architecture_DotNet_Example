using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;
using Sm.Crm.Infrastructure.Persistence.Common;

namespace Sm.Crm.Infrastructure.Persistence.Repositories;

public class CustomerRepository : BaseRepository<Customer, long>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Customer>?> GetAllWithUser(int page = 1, int pageCount = 10)
    {
        var entities = await _context.Customers
            .OrderByDescending(e => e.Id)
            .Skip((page - 1) * pageCount)
            .Take(pageCount)
            .Include(e => e.UserFk)
            .ToListAsync();
        return entities;
    }
}