using Gym.Api.Contexts.SharedContext.Abstractions.Endpoit;
using Gym.Api.Extensions;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EditMemberRequest = Gym.Domain.Contexts.AccountContext.UseCases.Edit.Request;


namespace Gym.Api.Contexts.AccountContext.UseCases.Edit
{
    public class Endpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/edit", HandleAsync)
                .WithName("Member: Edit the member informations")
                .WithSummary("Edit the member informations")
                .WithDescription("Edit the member informations")
                .WithOrder(4)
                .RequireAuthorization()
                .Produces<Task<IResult>>();
        }

        private static async Task<IResult> HandleAsync(
            IMediator mediator,
            ClaimsPrincipal user,            
            EditMemberRequest request
            )
        {
            try
            {
                request.Id = new Guid(user.Id());
                request.Email = user.Email();
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
