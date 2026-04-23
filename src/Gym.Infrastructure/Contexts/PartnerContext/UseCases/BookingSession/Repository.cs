using Gym.Infrastructure.Data;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;
using Microsoft.EntityFrameworkCore;
using Gym.Domain.Contexts.ActivitiesContext.ValueObjects;
using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.PartnerContext.specification;
using Gym.Domain.Contexts.PartnerContext.UseCases.BookingSessions.Contratcs;
using Gym.Domain.Contexts.SharedContext.Specifications.Abstractions;

namespace Gym.Infrastructure.Contexts.PartnerContext.UseCases.BookingSession
{
    public class Repository(AppDbContext context) : IRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Booking>> GetAllBookingsAsync(EBookingStatus requested, CancellationToken stoppingToken)
        {

            var bookings = await _context.Bookings
                .AsNoTracking()
                .Where(x => x.Status == requested).ToListAsync(stoppingToken);
            return bookings;
        }

        public async Task UpdateBookingRequestAsync(MemberBookingSessionEvent notification, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.
                            Where(
                                x =>
                                    x.ActivityId == notification.ActivityId
                                    &&
                                    x.MemberId == notification.MemberId
                                    &&
                                    x.PartnerId == notification.PartnerId

                                )
                            .FirstOrDefaultAsync();
            if (booking == null)
                throw new Exception("Booking not founded.");

            if (booking!.CancelAt != null)
                throw new Exception("Booking is canceled.");
            if (booking!.Status != EBookingStatus.Requested)
                throw new Exception("Booking in worng status!");
            booking.Status = EBookingStatus.Sent;
            try
            {

                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Booking?> GetBookingByBookingSpecification(BaseSpecification<Booking> spec,
            CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.
                Where(spec.Criteria)
                .FirstOrDefaultAsync();
            return booking;
        }

        public async Task UpdateBookingAsync(Booking booking, CancellationToken cancellationToken)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync(cancellationToken);        }
    }
}
