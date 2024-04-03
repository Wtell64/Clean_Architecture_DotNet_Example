using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Requests.Commands.UpdateRequest;
public class UpdateRequestCommand : IRequest<bool>
{
    public int? Id { get; set; }
    public Guid CustomerUserId { get; set; }
    public Guid EmployeeUserId { get; set; }
    public int RequestStatusId { get; set; }
    public string? Description { get; set; }
}
public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, bool>
{
    private readonly IRequestRepository _repository;
    private readonly IMapper _mapper;

    public UpdateRequestCommandHandler(IRequestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Request>(request);
        bool isSuccess = await _repository.Update(entity);
        return isSuccess;
    }
}