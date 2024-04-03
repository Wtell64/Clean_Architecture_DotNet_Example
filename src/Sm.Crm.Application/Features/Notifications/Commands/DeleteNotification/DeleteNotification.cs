using MediatR;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Notifications.Commands.DeleteNotification;

public record DeleteNotificationCommand(int Id) : IRequest<bool>;

public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNotificationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _unitOfWork.NotificationRepository.DeleteById(request.Id);
        return isSuccess;
    }
}