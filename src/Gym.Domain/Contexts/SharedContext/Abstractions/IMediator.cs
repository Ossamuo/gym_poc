namespace Gym.Domain.Contexts.SharedContext.Abstractions;

/// <summary>
/// The mediator interface which will be using for creating the mediator 
/// </summary>
public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request
        , CancellationToken cancellationToken = default);

    Task PublishAsync<TNotification>(TNotification notification
        , CancellationToken cancellationToken = default) 
        where TNotification : INotification;
}