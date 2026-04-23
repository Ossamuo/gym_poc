using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.SharedContext.Abstractions
{
    // Interface for handling notifications.
    // A notification is a message that is sent to multiple handlers,
    // and it does not expect a response.
    // It is used for events that occur in the system,
    // such as when a user is created or when an order is placed.
    public interface INotificationHandler<in TNotification>
        where TNotification : INotification
    {
        Task HandleAsync(TNotification notification);
    }
}
