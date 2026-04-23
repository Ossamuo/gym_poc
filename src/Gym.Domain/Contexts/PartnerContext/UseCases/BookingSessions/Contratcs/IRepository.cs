using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;
using Gym.Domain.Contexts.ActivitiesContext.ValueObjects;
using Gym.Domain.Contexts.PartnerContext.specification;
using Gym.Domain.Contexts.SharedContext.Specifications.Abstractions;

namespace Gym.Domain.Contexts.PartnerContext.UseCases.BookingSessions.Contratcs
{
    public interface IRepository
    {
        Task<List<ActivitiesContext.Entities.Booking>> GetAllBookingsAsync(EBookingStatus requested, CancellationToken stoppingToken);
        Task UpdateBookingRequestAsync(MemberBookingSessionEvent notification, CancellationToken cancellationToken);
        Task<Booking?> GetBookingByBookingSpecification(BaseSpecification<Booking> spec, CancellationToken cancellationToken);
        Task UpdateBookingAsync(Booking booking, CancellationToken cancellationToken);
    }
}
