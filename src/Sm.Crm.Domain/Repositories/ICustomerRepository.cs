using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer, long>
{
    Task<List<Customer>?> GetAllWithUser(int page = 1, int pageCount = 10);
}