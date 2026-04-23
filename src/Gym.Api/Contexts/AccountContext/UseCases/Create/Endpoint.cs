using Gym.Api.Contexts.SharedContext.Abstractions.Endpoit;
using Gym.Domain.Contexts.AccountContext.UseCases.Create.Events;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using CreateMemberRequest = Gym.Domain.Contexts.AccountContext.UseCases.Create.Request;

namespace Gym.Api.Contexts.AccountContext.UseCases.Create;

public class Endpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync).WithName("Member: Register")
            .WithSummary("Member: Register a Member")
            .WithDescription("Register a Member")
            .WithOrder(1)
            .Produces<Task<IResult>>();
    }

    private static async Task<IResult> HandleAsync(IMediator mediator, CreateMemberRequest request)
    {
        var result = await mediator.SendAsync(request, new CancellationToken());
        if (result.IsSuccess)
        {
            await mediator.PublishAsync(new MemberCreatedEvent
            {
                MemberId = result.Data!.Id,
                Email = result.Data!.Email,
                EmailVerificationCode = result.Data!.EmailVerificationCode
            });
            return TypedResults.Ok(result);
        }
        else
            return TypedResults.BadRequest(result);
        //return result.IsSuccess
        //    ? TypedResults.Ok(result)
        //    : TypedResults.BadRequest(result);
    }
}