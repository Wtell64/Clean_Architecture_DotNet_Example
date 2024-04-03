using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.DocumentTypes.Queries.GetDocumentType;
public record GetAllDocumentTypeQuery : IRequest<ICollection<DocumentTypeDto>>;

public class GetAllDocumentTypeQueryHandler : IRequestHandler<GetAllDocumentTypeQuery, ICollection<DocumentTypeDto>>
{
    private readonly IDocumentTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetAllDocumentTypeQueryHandler(IDocumentTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<DocumentTypeDto>> Handle(GetAllDocumentTypeQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAll().ToListAsync();
        return _mapper.Map<List<DocumentTypeDto>>(entities);
    }
}
