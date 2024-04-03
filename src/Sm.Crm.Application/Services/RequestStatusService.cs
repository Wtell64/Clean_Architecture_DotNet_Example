using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Services;

public class RequestStatusService : IRequestStatusService
{
    private readonly IApplicationDbContext _db;
    private readonly IRequestStatusRepository _requestStatusRepository;
    private readonly IMapper _mapper;

    public RequestStatusService(IApplicationDbContext db, IRequestStatusRepository requestStatusRepository, IMapper mapper)
    {
        _db = db;
        _requestStatusRepository = requestStatusRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<RequestStatusDto>>> GetAll()
    {
        var entities = await _requestStatusRepository.GetAll().ToListAsync();
        return Result<List<RequestStatusDto>>.Success(_mapper.Map<List<RequestStatusDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<RequestStatusDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _db.RequestStatuses
            .OrderByDescending(x => x.Id)
            .ProjectTo<RequestStatusDto>(_mapper.ConfigurationProvider);
        return await PaginatedResult<RequestStatusDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<RequestStatusDto?>> GetById(int id)
    {
        var entity = await _requestStatusRepository.GetById(id);
        return Result<RequestStatusDto?>.Success(_mapper.Map<RequestStatusDto>(entity));
    }

    public async Task<Result<int>> Create(CreateOrEditRequestStatusDto requeststatus)
    {
        var entity = _mapper.Map<RequestStatus>(requeststatus);
        var id = await _requestStatusRepository.Create(entity);
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrEditRequestStatusDto requeststatus)
    {
        var entity = _mapper.Map<RequestStatus>(requeststatus);
        bool isSucces = await _requestStatusRepository.Update(entity);
        if (isSucces)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not Updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSucces = await _requestStatusRepository.DeleteById(id);
        if (isSucces)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not Deleted");
    }
}