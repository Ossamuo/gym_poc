using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Domain.Contexts.ActivitiesContext.Entities;

namespace Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession.Contracts
{
    public interface IRepository
    {
        Task BookingClassAsync(Booking booking, CancellationToken cancellationToken);
        Task<bool> BookingClassExistsAsync(Booking booking, CancellationToken cancellationToken);
    }
}
