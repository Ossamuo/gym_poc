using Gym.Api.Contexts.SharedContext.Abstractions.Endpoit;
using Gym.Api.Extensions;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using ListActivitiesRequest = Gym.Domain.Contexts.ActivitiesContext.UseCases.List.Request;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Contexts.ActivitiesContext.UseCases.List
{
    public class Endpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
                .WithName("Activity:List Activity")
                .WithSummary("List Activity")
                .WithDescription("List Activity")
                .WithOrder(1)
                //.RequireAuthorization()
                .Produces<Task<IResult>>();
        }

        private static async Task<IResult> HandleAsync(
            IMediator mediator,
            ClaimsPrincipal user
            ,ListActivitiesRequest request
            )
        {
            try
            {
                request.MemberId = Guid.TryParse(user.Id(), out var memberId) ? memberId : Guid.Empty;
                var result = await mediator.SendAsync(request, new CancellationToken());
                return result.IsSuccess
                    ? TypedResults.Ok(result)
                    : TypedResults.BadRequest(result);
            }
            catch (Exception)
            {
                return TypedResults.InternalServerError();
            }

        }
    }
}
