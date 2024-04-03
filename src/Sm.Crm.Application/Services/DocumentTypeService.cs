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

public class DocumentTypeService : IDocumentTypeService
{
    private readonly IApplicationDbContext _db;
    private readonly IDocumentTypeRepository _documentTypeRepository;
    private readonly IMapper _mapper;

    public DocumentTypeService(IApplicationDbContext db, IDocumentTypeRepository documentTypeService, IMapper mapper)
    {
        _db = db;
        _documentTypeRepository = documentTypeService;
        _mapper = mapper;
    }

    public async Task<Result<List<DocumentTypeDto>>> GetAll()
    {
        var entities = await _documentTypeRepository.GetAll().ToListAsync();
        return Result<List<DocumentTypeDto>>.Success(_mapper.Map<List<DocumentTypeDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<DocumentTypeDto>> GetPaginated(PaginationRequest req)
    {
        var entities = _db.DocumentTypes
            .OrderByDescending(x => x.Id)
            .ProjectTo<DocumentTypeDto>(_mapper.ConfigurationProvider);
        return await PaginatedResult<DocumentTypeDto>.Create(entities.AsNoTracking(), req.PageNumber, req.PageSize);
    }

    public async Task<Result<DocumentTypeDto?>> GetById(int id)
    {
        var entity = await _documentTypeRepository.GetById(id);
        return Result<DocumentTypeDto?>.Success(_mapper.Map<DocumentTypeDto>(entity));
    }

     public async Task<Result<CreateOrEditDocumentTypeDto?>> GetFormById(int id)
    {
        var entity = await _documentTypeRepository.GetById(id);
        return Result<CreateOrEditDocumentTypeDto?>.Success(_mapper.Map<CreateOrEditDocumentTypeDto>(entity));
    }

    public async Task<Result<int>> Create(CreateOrEditDocumentTypeDto documenttype)
    {
        var entity = _mapper.Map<DocumentType>(documenttype);
        var id = await _documentTypeRepository.Create(entity);
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> Update(CreateOrEditDocumentTypeDto documenttype)
    {
        var entity = _mapper.Map<DocumentType>(documenttype);
        bool isSucces = await _documentTypeRepository.Update(entity);
        if (isSucces)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not Updated!");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        bool isSucces = await _documentTypeRepository.DeleteById(id);
        if (isSucces)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not Deleted");
    }
}