using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IRequestService
{
    Task<Result<List<RequestDto>>> GetAll();

    Task<PaginatedResult<RequestDto>> GetPaginatedNormal(PaginationRequest req);

    Task<PaginatedResult<RequestDto>> GetPaginated(PaginationRequest req);

    Task<Result<RequestDto>> GetById(int id);

    Task<Result<CreateOrEditRequestDto?>> GetFormById(int id);

    Task<Result<int>> Create(CreateOrEditRequestDto dto);

    Task<Result<bool>> Update(CreateOrEditRequestDto dto);

    Task<Result<bool>> Delete(int id);
}