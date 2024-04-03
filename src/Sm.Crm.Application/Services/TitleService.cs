using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Services;

public class TitleService : ITitleService
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TitleService(IApplicationDbContext db, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _db = db;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<TitleDto>>> GetAll()
    {
        var entities = await _unitOfWork.TitleRepository.GetList(false);
        return Result<List<TitleDto>>.Success(_mapper.Map<List<TitleDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<TitleDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _db.Titles
             .OrderByDescending(e => e.Id)
             .ProjectTo<TitleDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<TitleDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<TitleDto?>> GetById(int id)
    {
        var entity = await _unitOfWork.TitleRepository.GetById(id);
        return Result<TitleDto?>.Success(_mapper.Map<TitleDto>(entity));
    }

    public async Task<Result<CreateOrEditTitleDto?>> GetFormById(int id)
    {
        var entity = await _unitOfWork.TitleRepository.GetById(id);
        return Result<CreateOrEditTitleDto?>.Success(_mapper.Map<CreateOrEditTitleDto>(entity));
    }

    public async Task<Result<long>> Create(CreateOrEditTitleDto dto)
    {
        var entity = _mapper.Map<Title>(dto);
        var id = await _unitOfWork.TitleRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<long>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrEditTitleDto dto)
    {
        var entity = _mapper.Map<Title>(dto);
        bool isSuccess = await _unitOfWork.TitleRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _unitOfWork.TitleRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}