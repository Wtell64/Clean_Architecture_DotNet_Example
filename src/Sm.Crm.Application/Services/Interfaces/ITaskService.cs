using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface ITaskService
{
    Task<Result<List<TaskDto>>> GetAll();

    Task<PaginatedResult<TaskDto>> GetPaginated(PaginationRequest req);

    Task<Result<TaskDto?>> GetById(int id);

    Task<Result<CreateOrEditTaskDto?>> GetFormById(int id);

    Task<Result<int>> Create(CreateOrEditTaskDto customer);

    Task<Result<bool>> Update(CreateOrEditTaskDto customer);

    Task<Result<bool>> Delete(int id);
}