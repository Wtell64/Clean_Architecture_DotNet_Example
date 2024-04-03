using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IDocumentTypeService
{
    Task<Result<List<DocumentTypeDto>>> GetAll();

    Task<PaginatedResult<DocumentTypeDto>> GetPaginated(PaginationRequest req);

    Task<Result<DocumentTypeDto?>> GetById(int id);

    Task<Result<CreateOrEditDocumentTypeDto?>> GetFormById(int id);

    Task<Result<int>> Create(CreateOrEditDocumentTypeDto documenttype);

    Task<Result<bool>> Update(CreateOrEditDocumentTypeDto documenttype);

    Task<Result<bool>> Delete(int id);
}