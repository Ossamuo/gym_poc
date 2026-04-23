using Gym.Domain.Contexts.AccountContext.UseCases.Create.Events;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Application.Contexts.MarketingContext.Events.UserCreated
{
    public  class HandlerNotification : INotificationHandler<MemberCreatedEvent>
    {
        public async Task HandleAsync(MemberCreatedEvent notification)
        {
            await Task.Run(() => Console.WriteLine($"Sending e-mail for Marketing to {notification.Email}..."));
        }
    }
}
