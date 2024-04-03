using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Services;

public class SaleService : ISaleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<SaleDto>>> GetAllAsync()
    {
        var entities = await _unitOfWork.SaleRepository.GetAll().ToListAsync();
        return Result<List<SaleDto>>.Success(_mapper.Map<List<SaleDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<SaleDto>> GetPaginatedAsync(PaginationRequest request)
    {
        var entities = _unitOfWork.SaleRepository.GetAll()
            //.Include(e => e.EmployeeUserFk)
            .OrderByDescending(e => e.Id)
            .ProjectTo<SaleDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<SaleDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }

    public async Task<Result<SaleDto?>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.SaleRepository.GetAll().FirstOrDefaultAsync(e => e.Id == id);
        return Result<SaleDto?>.Success(_mapper.Map<SaleDto>(entity));
    }

    public async Task<Result<int>> CreateAsync(CreateOrEditSaleDto dto)
    {
        var entity = _mapper.Map<Sale>(dto);
        var id = await _unitOfWork.SaleRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> UpdateAsync(CreateOrEditSaleDto dto)
    {
        var entity = _mapper.Map<Sale>(dto);
        bool isSuccess = await _unitOfWork.SaleRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        bool isSuccess = await _unitOfWork.SaleRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}