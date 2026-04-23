using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.ActivitiesContext.ValueObjects;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.List;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.List.Contracts;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Contexts.ActivitiesContext.UseCases.List
{
    public class Repository(AppDbContext context) : IRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<List<Response>> GetActivitiesAsync(Request request, CancellationToken cancellationToken)
        {
            var activities = await _context
                .Activities
                .AsNoTracking()
                .Where(a => a.StartAdt >= request.StartAt)
                .GroupJoin(
                    _context.Bookings.AsNoTracking().Where(b => b.MemberId == request.MemberId),
                    activity => activity.Id,
                    booking => booking.ActivityId,
                    (activity, bookings) => new { activity, bookings }
                    )
                .SelectMany(
                    x => x.bookings.DefaultIfEmpty(),
                    (x, booking) => new { x.activity, Status = booking != null ? (EBookingStatus?)booking.Status : null })
                .ToListAsync(cancellationToken); //TODO: Use pagination!!!!
            var result = new List<Response>();
            foreach (var item in activities)
            {
                result.Add(new Response
                (
                    item.activity.Id,
                    item.activity.PartnerId,
                    item.activity.Name,
                    item.activity.Description,
                    item.activity.ImageUrl,
                    item.Status,
                    item.activity.StartAdt.ToString("o"),
                    item.activity.EndAdt.ToString("o")
                ));
                
            }
            return result;
        }
    }
}
