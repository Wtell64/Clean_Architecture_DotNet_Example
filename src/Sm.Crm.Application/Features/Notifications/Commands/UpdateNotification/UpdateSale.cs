using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Notifications.Commands.UpdateSale;

public class UpdateNotificationCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string Description { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}

public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Notification>(request);
        bool isSuccess = await _unitOfWork.NotificationRepository.Update(entity);
        return isSuccess;
    }
}