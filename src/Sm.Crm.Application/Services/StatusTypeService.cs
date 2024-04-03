using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Services;

public class StatusTypeService : IStatusTypeService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public StatusTypeService(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = db;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Create(CreateOrEditStatusTypeDto statusType)
    {
        var entity = _mapper.Map<StatusType>(statusType);
        var id = await _unitOfWork.StatusTypeRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _unitOfWork.StatusTypeRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }

    public async Task<Result<List<StatusTypeDto>>> GetAll()
    {
        var entities = _unitOfWork.StatusTypeRepository.GetAll();
        return Result<List<StatusTypeDto>>.Success(_mapper.Map<List<StatusTypeDto>>(entities).ToList());
    }

    public async Task<Result<StatusTypeDto?>> GetById(int id)
    {
        var entity = _unitOfWork.StatusTypeRepository.GetAll().FirstOrDefaultAsync(e => e.Id == id);
        return Result<StatusTypeDto?>.Success(_mapper.Map<StatusTypeDto>(entity));
    }

    public async Task<PaginatedResult<StatusTypeDto>> GetPaginated(PaginationRequest req)
    {
        var entityQuery = _unitOfWork.StatusTypeRepository.GetAll().OrderByDescending(c => c.Id);

        var totalEntity = await entityQuery.CountAsync();
        var pagedEntities = await entityQuery.Skip((req.PageNumber - 1) * req.PageSize).AsNoTracking().ToListAsync();
        var pagedDtos = _mapper.Map<List<StatusTypeDto>>(pagedEntities);

        return new PaginatedResult<StatusTypeDto>(pagedDtos, totalEntity, req.PageNumber, req.PageSize);
    }

    public async Task<Result<bool>> Update(CreateOrEditStatusTypeDto statusType)
    {
        var entity = _mapper.Map<StatusType>(statusType);
        bool isSuccess = await _unitOfWork.StatusTypeRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }
}