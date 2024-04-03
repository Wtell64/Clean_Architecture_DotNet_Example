using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IUserEmailService
{
    Task<Result<List<UserEmailDto>>> GetAll();

    Task<PaginatedResult<UserEmailDto>> GetPaginated(PaginationRequest req);

    Task<Result<UserEmailDto?>> GetById(int id);

    Task<Result<int>> Create(CreateOrEditUserEmailDto userAddress);

    Task<Result<bool>> Update(CreateOrEditUserEmailDto userAddress);

    Task<Result<bool>> Delete(int id);
}