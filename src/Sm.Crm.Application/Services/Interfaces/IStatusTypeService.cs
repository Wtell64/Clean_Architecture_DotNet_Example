using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IStatusTypeService
{
    Task<Result<List<StatusTypeDto>>> GetAll();

    Task<PaginatedResult<StatusTypeDto>> GetPaginated(PaginationRequest req);

    Task<Result<StatusTypeDto?>> GetById(int id);

    Task<Result<int>> Create(CreateOrEditStatusTypeDto statusType);

    Task<Result<bool>> Update(CreateOrEditStatusTypeDto statusType);

    Task<Result<bool>> Delete(int id);
}