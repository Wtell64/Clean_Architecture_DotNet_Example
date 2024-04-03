using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Notifications.Commands.CreateNotification;

public class CreateNotificationCommand : IRequest<int>
{
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string Description { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateNotificationCommand notification, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Notification>(notification);
        await _unitOfWork.NotificationRepository.Create(entity);
        return entity.Id;
    }

 
}