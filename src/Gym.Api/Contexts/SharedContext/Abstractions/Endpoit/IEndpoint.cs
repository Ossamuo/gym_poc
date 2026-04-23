namespace Gym.Api.Contexts.SharedContext.Abstractions.Endpoit;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}