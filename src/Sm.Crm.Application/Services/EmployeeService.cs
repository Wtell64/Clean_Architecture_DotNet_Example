using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeService(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<EmployeeDto>>> GetAll()
    {
        var entities = _unitOfWork.EmployeeRepository.GetAll();
        return Result<List<EmployeeDto>>.Success(_mapper.Map<List<EmployeeDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<EmployeeDto>> GetPaginated(PaginationRequest req)
    {
        var entityQuery = _unitOfWork.EmployeeRepository.GetAll()
           .Include(e => e.UserId)
           .OrderByDescending(c => c.Id);

        var totalEntity = await entityQuery.CountAsync();
        var pagedEntities = await entityQuery.Skip((req.PageNumber - 1) * req.PageSize).AsNoTracking().ToListAsync();
        var pagedDtos = _mapper.Map<List<EmployeeDto>>(pagedEntities);

        return new PaginatedResult<EmployeeDto>(pagedDtos, totalEntity, req.PageNumber, req.PageSize);
    }

    public async Task<Result<EmployeeDto?>> GetById(int id)
    {
        var entity = await _unitOfWork.EmployeeRepository.GetById(id);
        return Result<EmployeeDto?>.Success(_mapper.Map<EmployeeDto>(entity));
    }

    public async Task<Result<int>> Create(CreateOrUpdateEmployeeDto employee)
    {
        var entity = _mapper.Map<Employee>(employee);
        var id = await _unitOfWork.EmployeeRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrUpdateEmployeeDto employee)
    {
        var entity = _mapper.Map<Employee>(employee);
        bool isSuccess = await _unitOfWork.EmployeeRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _unitOfWork.EmployeeRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}