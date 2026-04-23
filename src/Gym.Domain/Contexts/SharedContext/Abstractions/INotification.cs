using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.SharedContext.Abstractions
{
    //Interface create for mark the notifications,
    //for example, when we want to send an email or a message to the user,
    //we can use this interface to mark the notification and then use it in the handler to send the notification.
    public interface INotification
    {
    }
}
