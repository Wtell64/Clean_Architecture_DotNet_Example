using MediatR;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Titles.Commands.DeleteTitle;

public record DeleteTitleCommand(int Id) : IRequest<bool>;

public class DeleteTitleCommandHandler : IRequestHandler<DeleteTitleCommand, bool>
{
    private readonly ITitleRepository _repository;

    public DeleteTitleCommandHandler(ITitleRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteTitleCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _repository.DeleteById(request.Id);
        return isSuccess;
    }
}