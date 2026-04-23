using Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession.Contracts;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.ActivitiesContext.ValueObjects;

namespace Gym.Application.Contexts.ActivitiesContext.UseCases.BookingSession
{
    public class Handler : IHandler<Request, Result<Response?>>
    {

        private readonly IRepository _repository;

        public Handler(IRepository repo)
        {
            _repository = repo;
        }

        public async Task<Result<Response?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
        {
            var booking = Booking.Create(request.MemberId, request.ActivityId, request.PartnerId, null, null);
            bool exists =  await _repository.BookingClassExistsAsync(booking, cancellationToken);
            if (exists)
                return new Result<Response?>(null, 500, "The booking class already exists");
            
            await _repository.BookingClassAsync(booking, cancellationToken);
            return new Result<Response?>
            {
                Data = new Response
                {
                    ReservationAccepted = true
                },
                Message = "Class booked successfully"
            };
        }
    }
}
