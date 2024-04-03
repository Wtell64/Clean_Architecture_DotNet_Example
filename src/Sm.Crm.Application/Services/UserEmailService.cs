using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Services;

public class UserEmailService : IUserEmailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserEmailService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<UserEmailDto>>> GetAll()
    {
        var entities = _unitOfWork.UserEmailRepository.GetAll();
        return Result<List<UserEmailDto>>.Success(_mapper.Map<List<UserEmailDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<UserEmailDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _unitOfWork.UserEmailRepository.GetAll()
             .Include(e => e.UserFk)
             .OrderByDescending(e => e.Id)
             .ProjectTo<UserEmailDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<UserEmailDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<UserEmailDto?>> GetById(int id)
    {
        var entity = await _unitOfWork.UserEmailRepository.GetAll(e => e.UserFk).FirstOrDefaultAsync(e => e.Id == id);
        return Result<UserEmailDto?>.Success(_mapper.Map<UserEmailDto>(entity));
    }

    public async Task<Result<CreateOrEditUserEmailDto?>> GetFormById(int id)
    {
        var entity = await _unitOfWork.UserEmailRepository.GetById(id);
        return Result<CreateOrEditUserEmailDto?>.Success(_mapper.Map<CreateOrEditUserEmailDto>(entity));
    }

    public async Task<Result<int>> Create(CreateOrEditUserEmailDto dto)
    {
        var entity = _mapper.Map<UserEmail>(dto);
        var id = await _unitOfWork.UserEmailRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrEditUserEmailDto dto)
    {
        var entity = _mapper.Map<UserEmail>(dto);
        bool isSuccess = await _unitOfWork.UserEmailRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _unitOfWork.UserEmailRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}