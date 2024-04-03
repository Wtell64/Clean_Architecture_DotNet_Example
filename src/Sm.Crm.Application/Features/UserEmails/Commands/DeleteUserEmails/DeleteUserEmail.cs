using MediatR;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserEmails.Commands.Delete;

public record DeleteUserEmailCommand(int Id) : IRequest<bool>;

public class DeleteEmailCommandHandler : IRequestHandler<DeleteUserEmailCommand, bool>
{
    private readonly IUserEmailRepository _userEmailRepository;

    public DeleteEmailCommandHandler(IUserEmailRepository userEmailRepository)
    {
        _userEmailRepository = userEmailRepository;
    }

    public async Task<bool> Handle(DeleteUserEmailCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _userEmailRepository.DeleteById(request.Id);
        return isSuccess;
    }
}