using Gym.Api.Contexts.SharedContext.Abstractions.Endpoit;
using Gym.Api.Extensions;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using AuthenticateMemberRequest =  Gym.Domain.Contexts.AccountContext.UseCases.Authenticate.Request;

namespace Gym.Api.Contexts.AccountContext.UseCases.Authenticate;

public class Endpoint :IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/authenticate", HandleAsync).
            WithName("Account: Authenticate the account")
            .WithSummary("Account: Authenticate the account")
            .WithDescription("Authenticate the account")
            .WithOrder(1)
            .Produces<Task<IResult>>();
    }

    private static async Task<IResult> HandleAsync(IMediator mediator, AuthenticateMemberRequest  request)
    {
        var result = await mediator.SendAsync(request, new CancellationToken());
        if (!result.IsSuccess)
            return TypedResults.BadRequest(result);

        result.Data!.Token = JwtExtension.Generate(result.Data);
        return TypedResults.Ok(result);
    }
}