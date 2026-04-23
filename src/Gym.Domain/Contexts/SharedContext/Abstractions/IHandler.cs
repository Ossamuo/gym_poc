namespace Gym.Domain.Contexts.SharedContext.Abstractions;

 public interface IHandler<in TRequest, TResponse>
     where TRequest : IRequest<TResponse>
 {
     Task<TResponse> HandlerAsync(TRequest request
         , CancellationToken cancellationToken = default);
 }