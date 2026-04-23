using EllipticCurve.Utils;
using Gym.Api.Contexts.SharedContext.Abstractions.Endpoit;
using Gym.Api.Extensions;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DetailMemberRequest = Gym.Domain.Contexts.AccountContext.UseCases.Detail.Request;

namespace Gym.Api.Contexts.AccountContext.UseCases.Detail
{
    public class Endpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Member:Get member Detail")
                .WithSummary("Get member Detail")
                .WithDescription("Get member Detail")
                .WithOrder(3)
                .RequireAuthorization()
                .Produces<Task<IResult>>();
        }

        private static async Task<IResult> HandleAsync(
            IMediator mediator,
            ClaimsPrincipal user
            )
        {
            try
            {
                //Get the user id from the claims and set it to the request 
                var request = new DetailMemberRequest();
                request.Id = new Guid(user.Id());

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
