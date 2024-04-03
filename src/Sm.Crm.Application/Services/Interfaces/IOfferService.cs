using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IOfferService
{
    Task<Result<List<OfferDto>>> GetAll();

    Task<PaginatedResult<OfferDto>> GetPaginated(PaginationRequest req);

    Task<Result<OfferDto?>> GetById(int id);

    Task<Result<CreateOrEditOfferDto?>> GetFormById(int id);

    Task<Result<int>> Create(CreateOrEditOfferDto offer);

    Task<Result<bool>> Update(CreateOrEditOfferDto offer);

    Task<Result<bool>> Delete(int id);
}