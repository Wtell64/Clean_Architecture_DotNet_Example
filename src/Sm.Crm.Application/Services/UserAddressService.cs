using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Services;

public class UserAddressService : IUserAddressService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserAddressService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<UserAddressDto>>> GetAll()
    {
        var entities = _unitOfWork.UserAddressRepository.GetAll();
        return Result<List<UserAddressDto>>.Success(_mapper.Map<List<UserAddressDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<UserAddressDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _unitOfWork.UserAddressRepository.GetAll()
             .Include(e => e.UserFk)
             .OrderByDescending(e => e.Id)
             .ProjectTo<UserAddressDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<UserAddressDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<UserAddressDto?>> GetById(int id)
    {
        var entity = await _unitOfWork.UserAddressRepository.GetAll(e => e.UserFk).FirstOrDefaultAsync(e => e.Id == id);
        return Result<UserAddressDto?>.Success(_mapper.Map<UserAddressDto>(entity));
    }

    public async Task<Result<CreateOrEditUserAddressDto?>> GetFormById(int id)
    {
        var entity = await _unitOfWork.UserAddressRepository.GetById(id);
        return Result<CreateOrEditUserAddressDto?>.Success(_mapper.Map<CreateOrEditUserAddressDto>(entity));
    }

    public async Task<Result<int>> Create(CreateOrEditUserAddressDto dto)
    {
        var entity = _mapper.Map<UserAddress>(dto);
        var id = await _unitOfWork.UserAddressRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrEditUserAddressDto dto)
    {
        var entity = _mapper.Map<UserAddress>(dto);
        bool isSuccess = await _unitOfWork.UserAddressRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _unitOfWork.UserAddressRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}