using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Features.Departments.Commands;
using Sm.Crm.Application.Features.Departments.Queries;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartmentService(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<DepartmentDto>>> GetAll()
    {
        var entities = await _unitOfWork.DepartmentRepository.GetAll().ToListAsync();
        return Result<List<DepartmentDto>>.Success(_mapper.Map<List<DepartmentDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<DepartmentDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _db.Departments
             .OrderByDescending(e => e.Id)
             .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<DepartmentDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<DepartmentDto?>> GetById(int id)
    {
        var entity = await _unitOfWork.DepartmentRepository.GetById(id);
        return Result<DepartmentDto?>.Success(_mapper.Map<DepartmentDto>(entity));
    }

    public async Task<Result<CreateOrUpdateDepartmentDto?>> GetFormById(int id)
    {
        var entity = await _unitOfWork.DepartmentRepository.GetById(id);
        return Result<CreateOrUpdateDepartmentDto?>.Success(_mapper.Map<CreateOrUpdateDepartmentDto>(entity));
    }

    public async Task<Result<int>> Create(CreateOrUpdateDepartmentDto dto)
    {
        var entity = _mapper.Map<Department>(dto);
        var id = await _unitOfWork.DepartmentRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrUpdateDepartmentDto dto)
    {
        var entity = _mapper.Map<Department>(dto);
        bool isSuccess = await _unitOfWork.DepartmentRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _unitOfWork.CustomerRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}