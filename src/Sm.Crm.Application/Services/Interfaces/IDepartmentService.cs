using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Features.Departments.Commands;
using Sm.Crm.Application.Features.Departments.Queries;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IDepartmentService
{
    Task<Result<List<DepartmentDto>>> GetAll();

    Task<PaginatedResult<DepartmentDto>> GetPaginated(PaginationRequest req);

    Task<Result<DepartmentDto?>> GetById(int id);

    Task<Result<CreateOrUpdateDepartmentDto?>> GetFormById(int id);

    Task<Result<int>> Create(CreateOrUpdateDepartmentDto department);

    Task<Result<bool>> Update(CreateOrUpdateDepartmentDto department);

    Task<Result<bool>> Delete(int id);
}