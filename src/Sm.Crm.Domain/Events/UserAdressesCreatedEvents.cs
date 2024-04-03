using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Domain.Events;
public class UserAdressesCreatedEvents:BaseEvent
{
    public UserAddress UserAddress { get; set; }
    public UserAdressesCreatedEvents(UserAddress userAddress)
    {
        UserAddress = userAddress;
    }
}