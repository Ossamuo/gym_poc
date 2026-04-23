using Gym.Api.Contexts.SharedContext.Abstractions.Endpoit;
using MemberCreateEndpoint = Gym.Api.Contexts.AccountContext.UseCases.Create.Endpoint;
using MemberDetailEndpoint = Gym.Api.Contexts.AccountContext.UseCases.Detail.Endpoint;
using MemberEditEndpoint = Gym.Api.Contexts.AccountContext.UseCases.Edit.Endpoint;
using MemberAuthenticateEndpoint = Gym.Api.Contexts.AccountContext.UseCases.Authenticate.Endpoint;

using ActivityListtEndpoint = Gym.Api.Contexts.ActivitiesContext.UseCases.List.Endpoint;
using ActivityBookEndpoint = Gym.Api.Contexts.ActivitiesContext.UseCases.Booking.Endpoint;
using PartnerBookEndpoint = Gym.Api.Contexts.PatnerContext.UseCases.Bokking.Endoint;


namespace Gym.Api.Endpoints;

public static class Endpoint
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("api/v1/members")
            .WithTags("Members")
            .MapEndpoint<MemberCreateEndpoint>()
            .MapEndpoint<MemberAuthenticateEndpoint>()
            .MapEndpoint<MemberDetailEndpoint>()
            .MapEndpoint<MemberEditEndpoint>()
            ;
        
        endpoints.MapGroup("api/v1/activities")
            .WithTags("activities")
            .MapEndpoint<ActivityListtEndpoint>()
            .MapEndpoint<ActivityBookEndpoint>()

            ; endpoints.MapGroup("api/v1/partners")
            .WithTags("partners")
            .MapEndpoint<PartnerBookEndpoint>()

            ;
    }
    private static IEndpointRouteBuilder MapEndpoint<TEndoint>(this IEndpointRouteBuilder app)
        where TEndoint : IEndpoint

    {
        TEndoint.Map(app);
        return app;
    }
}