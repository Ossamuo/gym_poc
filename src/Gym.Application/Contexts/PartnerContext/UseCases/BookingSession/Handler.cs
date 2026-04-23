using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.ActivitiesContext.ValueObjects;
using Gym.Domain.Contexts.PartnerContext.specification;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.PartnerContext.UseCases.BookingSessions;
using Gym.Domain.Contexts.PartnerContext.UseCases.BookingSessions.Contratcs;
using Gym.Domain.Contexts.SharedContext.Results;

namespace Gym.Application.Contexts.PartnerContext.UseCases.BookingSession
{
    //for a microservice approach this handler should only save the message in specific Table
    //and also it should be ideal implement Idempotency for ensure a message should be process only on time
    
    public class Handler(IRepository repository): IHandler<Request, Result<Response?>>
    {
        public async Task<Result<Response?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
        {
            var spec = new GetByPartnerIdActivityIdMemberId(request.PartnerId, request.ActivityId, request.MemberId);
            var booking = await repository.GetBookingByBookingSpecification(spec, cancellationToken);
            if (booking == null)
                return new Result<Response?>(null, 404,"Booking Not found");
            
            if (Enum.TryParse<EBookingStatus>(request.Status.Trim(), ignoreCase: true, out EBookingStatus result))
                booking.Status = result;
            else
            {
                return new Result<Response?>(null, 500,"Invalid status");
            }
            if( booking.Status == EBookingStatus.Requested || booking.Status == EBookingStatus.Sent)
                return new Result<Response?>(null, 500,"Invalid status");
            await repository.UpdateBookingAsync(booking, cancellationToken);
            
            return new Result<Response?>(null, 200,"Updated");
        }
    }
}
