using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities.LST;
using Sm.Crm.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.RequestStatuses.Commands.UpdateRequestStatus;
public class UpdateRequestStatusCommand : IRequest<bool>
{
    public int? Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateRequestStatusCommandHandler : IRequestHandler<UpdateRequestStatusCommand, bool>
{
    private readonly IRequestStatusRepository _repository;
    private readonly IMapper _mapper;

    public UpdateRequestStatusCommandHandler(IRequestStatusRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateRequestStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<RequestStatus>(request);
        bool isSuccess = await _repository.Update(entity);
        return isSuccess;
    }
}
