using MediatR;
using Microsoft.Extensions.Logging;
using Sm.Crm.Application.Features.Customers.EventHandlers;
using Sm.Crm.Domain.Events;

namespace Sm.Crm.Application.Features.UserAddresses.EventHandlers;
public class UserAdressesCreatedEventHandler : INotificationHandler<UserAdressesCreatedEvents>
{
    private readonly ILogger<UserAdressesCreatedEvents> _logger;

    public UserAdressesCreatedEventHandler(ILogger<UserAdressesCreatedEvents> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserAdressesCreatedEvents notification, CancellationToken cancellationToken)
    {
        var eventName = notification.GetType().Name;
        _logger.LogInformation($"UserAdressesCreatedEventHandler is working. Event: {eventName}");
        return Task.CompletedTask;
    }
}
