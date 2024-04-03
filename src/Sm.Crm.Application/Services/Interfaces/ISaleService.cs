using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface ISaleService
{
    Task<Result<List<SaleDto>>> GetAllAsync();

    Task<PaginatedResult<SaleDto>> GetPaginatedAsync(PaginationRequest request);

    Task<Result<SaleDto?>> GetByIdAsync(int id);

    Task<Result<int>> CreateAsync(CreateOrEditSaleDto dto);

    Task<Result<bool>> UpdateAsync(CreateOrEditSaleDto dto);

    Task<Result<bool>> DeleteAsync(int id);
}