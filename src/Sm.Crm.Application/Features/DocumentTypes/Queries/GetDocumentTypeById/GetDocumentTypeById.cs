using AutoMapper;
using MediatR;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Features.Titles.Queries.GetAllTitles;
using Sm.Crm.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.DocumentTypes.Queries.GetDocumentTypeById;
public class GetDocumentTypeByIdQuery : IRequest<DocumentTypeDto>
{
    public int Id { get; set; }

    public GetDocumentTypeByIdQuery(int id)
    {
       Id = id;
    }
}
public class GetDocmuentTypeByIdQueryHandler : IRequestHandler<GetDocumentTypeByIdQuery, DocumentTypeDto?>
{
    private readonly IDocumentTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetDocmuentTypeByIdQueryHandler(IDocumentTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DocumentTypeDto?> Handle(GetDocumentTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        return _mapper.Map<DocumentTypeDto>(entity);
    }
}
