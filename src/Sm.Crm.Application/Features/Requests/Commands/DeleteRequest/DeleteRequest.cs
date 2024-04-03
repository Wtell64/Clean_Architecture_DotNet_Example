using MediatR;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Requests.Commands.DeleteRequest;
public record DeleteRequestCommand(int Id) : IRequest<bool>;

public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, bool>
{
    private readonly IRequestRepository _requestRepository;

    public DeleteRequestCommandHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<bool> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _requestRepository.DeleteById(request.Id);
        return isSuccess;
    }
}