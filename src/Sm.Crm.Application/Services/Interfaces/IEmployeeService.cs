using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IEmployeeService
{
    Task<Result<List<EmployeeDto>>> GetAll();

    Task<PaginatedResult<EmployeeDto>> GetPaginated(PaginationRequest req);

    Task<Result<EmployeeDto?>> GetById(int id);

    Task<Result<int>> Create(CreateOrUpdateEmployeeDto employee);

    Task<Result<bool>> Update(CreateOrUpdateEmployeeDto employee);

    Task<Result<bool>> Delete(int id);
}