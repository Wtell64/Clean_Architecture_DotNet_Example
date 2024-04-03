using AutoMapper;
using MediatR;
using Sm.Crm.Application.Features.Titles.Commands.UpdateTitle;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.DocumentTypes.Commands.UpdateDocumentType;
public class UpdateDocumentTypeCommand : IRequest<bool>
{
    public int? Id { get; set; }
    public string? Name { get; set; }
}
public class UpdateDocumentTypeCommandHandler : IRequestHandler<UpdateDocumentTypeCommand, bool>
{
    private readonly IDocumentTypeRepository _repository;
    private readonly IMapper _mapper;

    public UpdateDocumentTypeCommandHandler(IDocumentTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<DocumentType>(request);
        bool isSuccess = await _repository.Update(entity);
        return isSuccess;
    }
}
