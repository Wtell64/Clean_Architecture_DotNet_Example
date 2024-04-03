using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Requests.Commands.CreateRequest;
public class CreateRequestCommand : IRequest<int>
{
    public Guid CustomerUserId { get; set; }
    public Guid EmployeeUserId { get; set; }
    public int RequestStatusId { get; set; }
    public string? Description { get; set; }
}

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, int>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMapper _mapper;

    public CreateRequestCommandHandler(IRequestRepository requestRepository, IMapper mapper)
    {
        _requestRepository = requestRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Request>(request);
        var id = await _requestRepository.Create(entity);

        return id;
    }
}