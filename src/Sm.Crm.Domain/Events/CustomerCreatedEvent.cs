using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Domain.Events;

public class CustomerCreatedEvent : BaseEvent
{
    public Customer Customer { get; }

    public CustomerCreatedEvent(Customer customer)
    {
        Customer = customer;
    }
}