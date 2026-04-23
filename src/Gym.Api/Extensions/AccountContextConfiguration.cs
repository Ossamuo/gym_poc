using Gym.Application.Contexts.SharedContext.Results;
using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using AuthenticateMemberRequest =  Gym.Domain.Contexts.AccountContext.UseCases.Authenticate.Request;
using CreateMemberRequest =  Gym.Domain.Contexts.AccountContext.UseCases.Create.Request;
namespace Gym.Api.Extensions;

public static class AccountContextConfiguration
{
    public static void AddAccountContextConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddTransient<
                Gym.Domain.Contexts.AccountContext.UseCases.Create.Contracts.IRepository
                , Gym.Infrastructure.Contexts.AccountContext.UseCases.Create.Repository>();
        
        builder.Services
            .AddTransient<
                Gym.Domain.Contexts.AccountContext.UseCases.Create.Contracts.IService
                , Gym.Infrastructure.Contexts.AccountContext.UseCases.Create.Services>();
        
        
        builder.Services
            .AddTransient<
                Gym.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository
                , Gym.Infrastructure.Contexts.AccountContext.UseCases.Authenticate.Repository>();

        builder.Services
            .AddTransient<
                Gym.Domain.Contexts.AccountContext.UseCases.Detail.Contracts.IRepository
                , Gym.Infrastructure.Contexts.AccountContext.UseCases.Detail.Repository>();

        builder.Services
        .AddTransient<
            Gym.Domain.Contexts.AccountContext.UseCases.Edit.Contracts.IRepository
            , Gym.Infrastructure.Contexts.AccountContext.UseCases.Edit.Repository>();

    }
    // public static void MapAccountEndpoints(this WebApplication  app)
    // {
    //     // app.MapPost("api/v1/members",  async  Task<IResult> (IMediator mediator, CreateMemberRequest request) =>
    //     // {
    //     //     var result = await mediator.SendAsync(request, new CancellationToken());
    //     //     return result.IsSuccess? 
    //     //            TypedResults.Ok(result) :
    //     //     TypedResults.BadRequest(result);
    //     // });
    //     
    //     // app.MapPost("api/v1/authenticate",  async  Task<IResult> (IMediator mediator, AuthenticateMemberRequest  request) =>
    //     // {
    //     //     var result = await mediator.SendAsync(request, new CancellationToken());
    //     //     if (!result.IsSuccess)
    //     //      return TypedResults.BadRequest(result);
    //     //
    //     //     result.Data.Token = JwtExtension.Generate(result.Data);
    //     //     return TypedResults.Ok(result);
    //     //
    //     // });
    // }
}