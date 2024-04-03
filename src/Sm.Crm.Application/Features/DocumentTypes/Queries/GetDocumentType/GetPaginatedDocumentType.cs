using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Features.Titles.Queries.GetAllTitles;
using Sm.Crm.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.DocumentTypes.Queries.GetDocumentType;
public class GetPaginatedDocumentTypesQuery : IRequest<PaginatedResult<DocumentTypeDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedDocumentTypesQueryHandler : IRequestHandler<GetPaginatedDocumentTypesQuery, PaginatedResult<DocumentTypeDto>>
{
    private readonly IDocumentTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetPaginatedDocumentTypesQueryHandler(IDocumentTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<DocumentTypeDto>> Handle(GetPaginatedDocumentTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = _repository.GetAll()
             .OrderByDescending(e => e.Id)
             .ProjectTo<DocumentTypeDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<DocumentTypeDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}
