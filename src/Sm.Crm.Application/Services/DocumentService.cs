using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _repository;
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public DocumentService(IDocumentRepository repository, IApplicationDbContext db, IMapper mapper)
    {
        _repository = repository;
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<int>> Create(CreateOrEditDocumentDto dto)
    {
        var entity = _mapper.Map<Document>(dto);
        var id = await _repository.Create(entity);
        //await _repository.CommitAsync(id);
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSuccess = await _repository.DeleteById(id);
        //await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }

    public async Task<Result<List<DocumentDto>>> GetAll()
    {
        var entities = await _repository.GetAll().ToListAsync();
        return Result<List<DocumentDto>>.Success(_mapper.Map<List<DocumentDto>>(entities).ToList());
    }

    public async Task<Result<DocumentDto?>> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return Result<DocumentDto?>.Success(_mapper.Map<DocumentDto>(entity));
    }

    public async Task<Result<CreateOrEditDocumentDto?>> GetFormById(int id)
    {
        var entity = await _repository.GetById(id);
        return Result<CreateOrEditDocumentDto?>.Success(_mapper.Map<CreateOrEditDocumentDto>(entity));
    }

    public async Task<PaginatedResult<DocumentDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _db.Documents
             .OrderByDescending(e => e.Id)
             .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<DocumentDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<bool>> Update(CreateOrEditDocumentDto dto)
    {
        var entity = _mapper.Map<Document>(dto);
        bool isSuccess = await _repository.Update(entity);
        //await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }
}