using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface ITitleService
{
    Task<Result<List<TitleDto>>> GetAll();

    Task<PaginatedResult<TitleDto>> GetPaginated(PaginationRequest req);

    Task<Result<TitleDto?>> GetById(int id);

    Task<Result<CreateOrEditTitleDto?>> GetFormById(int id);

    Task<Result<long>> Create(CreateOrEditTitleDto title);

    Task<Result<bool>> Update(CreateOrEditTitleDto title);

    Task<Result<bool>> Delete(int id);
}