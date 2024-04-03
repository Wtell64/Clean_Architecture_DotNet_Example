using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Notifications.Queries.GetNotification;
public class GetPaginatedNotificationsQuery : IRequest<PaginatedResult<NotificationDto>>
{
    public string? Search { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedNotificationsQueryHandler : IRequestHandler<GetPaginatedNotificationsQuery, PaginatedResult<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public GetPaginatedNotificationsQueryHandler(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<NotificationDto>> Handle(GetPaginatedNotificationsQuery request, CancellationToken cancellationToken)
    {
        var entities = _notificationRepository.GetAll()
            .Include(e => e.UserId)
            .Include(e => e.Title)
            .Include(e => e.Description)
            .OrderByDescending(e => e.Id)
            .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<NotificationDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}