using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IUserAddressService
{
    Task<Result<List<UserAddressDto>>> GetAll();

    Task<PaginatedResult<UserAddressDto>> GetPaginated(PaginationRequest req);

    Task<Result<UserAddressDto?>> GetById(int id);

    Task<Result<CreateOrEditUserAddressDto?>> GetFormById(int id);

    Task<Result<int>> Create(CreateOrEditUserAddressDto userAddress);

    Task<Result<bool>> Update(CreateOrEditUserAddressDto userAddress);

    Task<Result<bool>> Delete(int id);
}