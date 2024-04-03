using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Notifications.Queries.GetNotification;
public class GetAllNotificationsQuery : IRequest<ICollection<NotificationDto>>;

public class GetAllNotificationsQeuryHandler : IRequestHandler<GetAllNotificationsQuery, ICollection<NotificationDto>>
{

    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public GetAllNotificationsQeuryHandler(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<NotificationDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
        var entities = _notificationRepository.GetAll()
            .Include(e => e.UserId)
            .Include(e => e.Title)
            .Include(e => e.Description);

        var list = await entities.ToListAsync();

        return _mapper.Map<List<NotificationDto>>(list);
    }
}