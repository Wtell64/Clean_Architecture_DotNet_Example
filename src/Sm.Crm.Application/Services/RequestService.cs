using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Services;

public class RequestService : IRequestService
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RequestService(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<RequestDto>>> GetAll()
    {
        var entities = _unitOfWork.RequestRepository.GetAll();
        return Result<List<RequestDto>>.Success(_mapper.Map<List<RequestDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<RequestDto>> GetPaginatedNormal(PaginationRequest req)
    {
        var entityQuery = _unitOfWork.RequestRepository.GetAll()
           .Include(e => e.CustomerFk).ThenInclude(e => e.UserFk)
           .Include(e => e.EmployeeFk).ThenInclude(e => e.UserFk)
           .OrderByDescending(c => c.Id);

        var totalEntity = await entityQuery.CountAsync();
        var pagedEntities = await entityQuery.Skip((req.PageNumber - 1) * req.PageSize).AsNoTracking().ToListAsync();
        var pagedDtos = _mapper.Map<List<RequestDto>>(pagedEntities);

        return new PaginatedResult<RequestDto>(pagedDtos, totalEntity, req.PageNumber, req.PageSize);
    }

    public async Task<PaginatedResult<RequestDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _unitOfWork.RequestRepository.GetAll()
            .Include(e => e.CustomerFk).ThenInclude(e => e.UserFk)
            .Include(e => e.EmployeeFk).ThenInclude(e => e.UserFk)
            .OrderByDescending(e => e.Id)
            .ProjectTo<RequestDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<RequestDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<RequestDto>> GetById(int id)
    {
        var entity = await _unitOfWork.RequestRepository.GetAll().FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
        {
            return Result<RequestDto>.Failure("Request not found.");
        }
        return Result<RequestDto>.Success(_mapper.Map<RequestDto>(entity));
    }

    public async Task<Result<CreateOrEditRequestDto?>> GetFormById(int id)
    {
        var entity = await _unitOfWork.RequestRepository.GetById(id);
        return Result<CreateOrEditRequestDto?>.Success(_mapper.Map<CreateOrEditRequestDto>(entity));
    }

    public async Task<Result<int>> Create(CreateOrEditRequestDto dto)
    {
        var entity = _mapper.Map<Request>(dto);
        var id = await _unitOfWork.RequestRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrEditRequestDto dto)
    {
        var entity = _mapper.Map<Request>(dto);
        bool isSuccess = await _unitOfWork.RequestRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var isSuccess = await _unitOfWork.RequestRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        return isSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure("Request not deleted.");
    }
}