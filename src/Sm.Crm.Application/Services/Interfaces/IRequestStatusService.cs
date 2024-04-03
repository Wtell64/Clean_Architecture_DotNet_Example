using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IRequestStatusService
{
    Task<Result<List<RequestStatusDto>>> GetAll();

    Task<PaginatedResult<RequestStatusDto>> GetPaginated(PaginationRequest req);

    Task<Result<RequestStatusDto?>> GetById(int id);

    Task<Result<int>> Create(CreateOrEditRequestStatusDto requestStatus);

    Task<Result<bool>> Update(CreateOrEditRequestStatusDto requestStatus);

    Task<Result<bool>> Delete(int id);
}