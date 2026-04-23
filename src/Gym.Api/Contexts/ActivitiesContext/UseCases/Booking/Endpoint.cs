using Azure.Core;
using Gym.Api.Contexts.SharedContext.Abstractions.Endpoit;
using Gym.Api.Extensions;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Gym.Domain.Contexts.SharedContext.Metrics;
using BookActivityRequest = Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession.Request;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;

namespace Gym.Api.Contexts.ActivitiesContext.UseCases.Booking
{
    public class Endpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/booking", HandleAsync)
                .WithName("Activity:Book Activity")
                .WithSummary("Book Activity")
                .WithDescription("Book Activity")
                .WithOrder(2)
                .RequireAuthorization()
                .Produces<Task<IResult>>();
        }

        private static async Task<IResult> HandleAsync(
            IMediator mediator
            , ClaimsPrincipal user
            , [FromBody] BookActivityRequest request)
        {
            try
            {
                request.MemberId = new Guid(user.Id());
                var result = await mediator.SendAsync(request, new CancellationToken());
                if (result.IsSuccess)
                {
                    //NOTE : If the booking is successful, we publish an event to notify other parts of the system about the new booking.
                    //NOTE:
                    //  The fragility is if the notification fails, i might lead to inconsistencies in the system.
                    //  Other point is if there are multiple events to publish, it can lead to performance issues.


                    BusinessMetrics.BookingCounter.Add(1,
                        new TagList { { "status", "requested" }, { "member_type", request.MemberId } });
                    await mediator.PublishAsync(new MemberBookingSessionEvent
                    {
                        MemberId = request.MemberId,
                        ActivityId = request.ActivityId,
                        PartnerId = request.PartnerId
                    });
                    return TypedResults.Ok(result);
                }
                else
                {
                    BusinessMetrics.BookingCounter.Add(1,
                        new TagList { { "status", "BadRequest" }, { "member_type", request.MemberId } });
                    return TypedResults.BadRequest(result);
                }
            }
            catch (Exception)
            {
                //TODO log the exception
                BusinessMetrics.BookingCounter.Add(1,
                    new TagList { { "status", "InternalServerError" }, { "member_type", request.MemberId } });
                return TypedResults.InternalServerError();
            }
        }
    }
}