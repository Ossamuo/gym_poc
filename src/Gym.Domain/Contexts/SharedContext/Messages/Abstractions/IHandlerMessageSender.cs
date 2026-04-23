using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.SharedContext.Messages.Abstractions
{
    public interface IHandlerMessageSender<in T>
    {
        Task SendAsync(T message);
    }
}
