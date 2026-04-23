using Gym.Domain.Contexts.AccountContext.UseCases.Create.Events;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;
using Gym.Domain.Contexts.PartnerContext.UseCases.Events.MemberBookingSession.Constracts;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Application.Contexts.PartnerContext.UseCases.Events.MemberBookingSession
{
    public class HandlerNotification(IServices services) : INotificationHandler<MemberBookingSessionEvent>
    {
        public async Task HandleAsync(MemberBookingSessionEvent notification)
        {
            await services.SendBookReservationAsync(notification, new CancellationToken());
        }
    }
}

