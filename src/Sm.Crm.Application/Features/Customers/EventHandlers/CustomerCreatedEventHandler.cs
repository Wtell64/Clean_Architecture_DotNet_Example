using MediatR;
using Microsoft.Extensions.Logging;
using Sm.Crm.Domain.Events;

namespace Sm.Crm.Application.Features.Customers.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedEventHandler> _logger;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        //LOG
        var eventName = notification.GetType().Name;
        _logger.LogInformation($"CustomerCreatedEventHandler is working. Event: {eventName}");
        return Task.CompletedTask;

        //NOTIFY
        //_smsService.SendSms(notification.Customer.Name, notification.Customer.Phone);

        //DB
        //LogDb.Create(notification.Customer.Id, notification.Customer.Name, notification.Customer.Phone);
    }
}