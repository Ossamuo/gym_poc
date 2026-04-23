using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;
using Gym.Domain.Contexts.ActivitiesContext.ValueObjects;
using Gym.Domain.Contexts.PartnerContext.UseCases.BookingSessions.Contratcs;
using Gym.Domain.Contexts.PartnerContext.UseCases.Events.MemberBookingSession.Constracts;

namespace Gym.BookingMessage.Producer.Workers;

public class BookingProducerWorker(
    IServiceScopeFactory scopeFactory,
    ILogger<BookingProducerWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //TODO create a handler for this part
            //this logic should be in another project like Gym.Application.Microservices 
            //in order to isolate the logic from Gym.Api and  Gym.BookingMessage.Producer 
            //and also for a microservice approach It should have a table for message to deal with Idempotency 

            await using var scope = scopeFactory.CreateAsyncScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepository>();
            var service = scope.ServiceProvider.GetRequiredService<IServices>();

            var bookings = await repository.GetAllBookingsAsync(EBookingStatus.Requested, stoppingToken);

            logger.LogInformation("Found {Count} booking(s) with status Requested.", bookings.Count);

            foreach (var booking in bookings)
            {
                var notification = new MemberBookingSessionEvent
                {
                    PartnerId = booking.PartnerId,
                    ActivityId = booking.ActivityId,
                    MemberId = booking.MemberId
                };

                await service.SendBookReservationAsync(notification, stoppingToken);
                await repository.UpdateBookingRequestAsync(notification, stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
