using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Events;
using Sm.Crm.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.DocumentTypes.Commands.CreateDocumentType;
public class CreateDocumentTypeCommand : IRequest<int>
{
    public string? Name { get; set; }    
}

public class CreateDocumentTypeCommandHandler : IRequestHandler<CreateDocumentTypeCommand, int>
{
    private readonly IDocumentTypeRepository _repository;
    private readonly IMapper _mapper;

    public CreateDocumentTypeCommandHandler(IDocumentTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<DocumentType>(request);
        var id = await _repository.Create(entity);
        return id;
    }
}