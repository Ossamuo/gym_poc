using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.PartnerContext.UseCases.Events.MemberBookingSession.Constracts
{
    public interface IServices
    {
        Task SendBookReservationAsync(MemberBookingSessionEvent notification, CancellationToken cancellationToken);
    }
}
