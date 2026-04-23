
using Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession.Contracts;
using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Contexts.ActivitiesContext.UseCases.BookingSession
{
    public class Repository  (AppDbContext dbContext): IRepository
    {
        private readonly AppDbContext _appDbContext = dbContext;
    
        public async Task BookingClassAsync(Booking  booking, CancellationToken cancellationToken)
        {
            await _appDbContext.Bookings.AddAsync(booking, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            
        }

        public Task<bool> BookingClassExistsAsync(Booking booking, CancellationToken cancellationToken)
        {
            return _appDbContext.Bookings.AsNoTracking().AnyAsync(
                x=> 
                    x.ActivityId ==booking.ActivityId 
                    && x.PartnerId == booking.PartnerId
                    && x.MemberId == booking.MemberId
                , cancellationToken);
        }
    }
}
