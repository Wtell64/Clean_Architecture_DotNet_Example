using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface IDocumentService
{
    Task<Result<List<DocumentDto>>> GetAll();

    Task<PaginatedResult<DocumentDto>> GetPaginated(PaginationRequest req);

    Task<Result<DocumentDto?>> GetById(int id);

    Task<Result<CreateOrEditDocumentDto?>> GetFormById(int id);

    Task<Result<int>> Create(CreateOrEditDocumentDto dto);

    Task<Result<bool>> Update(CreateOrEditDocumentDto dto);

    Task<Result<bool>> Delete(int id);
}