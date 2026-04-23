using Gym.Api.Contexts.SharedContext.Abstractions.Endpoit;
using Gym.Api.Extensions;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Gym.Domain.Contexts.SharedContext.Metrics;
using BookActivityRequest = Gym.Domain.Contexts.PartnerContext.UseCases.BookingSessions.Request;


namespace Gym.Api.Contexts.PatnerContext.UseCases.Bokking
{
    //This should be in an another API exclusive for Administratives use cases.
    public class Endoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/Booking", HandleAsync)
                .WithName("Partner:Response of Booking Request")
                .WithSummary("Response of Booking Request")
                .WithDescription("Response of Booking Request")
                .WithOrder(2)
                .RequireAuthorization(PartnerContextConfiguration.Policy)
                .Produces<Task<IResult>>();
        }

        private static async Task<IResult> HandleAsync(
            IMediator mediator,
            [FromBody] BookActivityRequest request)
        {
            try
            {
                await Task.Run(() => Console.WriteLine("Processing the reponse of the member booking request"));
                
                var result = await mediator.SendAsync(request, new CancellationToken());

                BusinessMetrics.BookingCounter.Add(1,
                    new TagList { { "status", request.Status }, { "member_type", request.MemberId } });
                if (result.IsSuccess)
                    return TypedResults.Ok();
                else
                    return TypedResults.BadRequest(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}