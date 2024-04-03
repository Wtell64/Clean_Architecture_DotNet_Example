using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface ICustomerService
{
    Task<Result<List<CustomerDto>>> GetAll();

    Task<PaginatedResult<CustomerDto>> GetPaginated(PaginationRequest req);

    Task<Result<CustomerDto?>> GetById(long id);

    Task<Result<CreateOrUpdateCustomerDto?>> GetFormById(long id);

    Task<Result<long>> Create(CreateOrUpdateCustomerDto customer);

    Task<Result<bool>> Update(CreateOrUpdateCustomerDto customer);

    Task<Result<bool>> Delete(long id);
}