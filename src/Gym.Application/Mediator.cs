using Gym.Domain.Contexts.SharedContext.Abstractions;

namespace Gym.Application;

public class Mediator(IServiceProvider provider) : IMediator
{
    public async Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        var notificationType = notification.GetType();

        // Creates IEnumerable<INotificationHandler<T>>
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);
        var enumerableHandlerType = typeof(IEnumerable<>).MakeGenericType(handlerType);

        // Search for all handlers that implement INotificationHandler<T> for the given notification type
        IEnumerable<object>? handlers = (IEnumerable<object>)provider.GetService(enumerableHandlerType);

        if (handlers != null)
        {
            foreach (var handler in handlers)
            {
                var method = handler.GetType().GetMethod("HandleAsync");
                //Issue if one of the handlers does not have the HandleAsync method, it will throw an exception.
                //and it will stop the execution of the other handlers, which is not ideal.
                if (method == null)
                {
                    Console.WriteLine($"HandlerAsync method not found in HandlerNotification type {handler.GetType()}.");
                    //throw new InvalidOperationException($"HandlerAsync method not found in HandlerNotification type {handler.GetType()}.");
                    continue;
                }
                try
                {
                    await Task.Run(() => method.Invoke(handler, new object[] { notification }));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        //first we need to get the type of the request
        var requestType = request.GetType();

        //then we need to get the type of the handler for this request

        var handlerType = typeof(IHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        //then we need to get the handler from the service provider

        //this scenario is not ideal because we are using reflection to get the handler and invoke it
        //but if the handler needs D.I it will be resulting in error. 
        //handlerType.GetMethod("HandlerAsync")?.Invoke(handlerType, new object[] { request, cancellationToken });


        //this scenario is ideal because we are using the service provider
        //and it will resolve the dependencies of the handler and invoke it 
        var handler = provider.GetService(handlerType);
        if (handler == null) 
            throw new InvalidOperationException($"Handler for request type {requestType.Name} not found.");

        var method = handlerType.GetMethod("HandlerAsync");

        if (method == null)
            throw new InvalidOperationException($"HandlerAsync method not found in handler for request type {requestType.Name}.");

        var result =  method.Invoke(handler, new object[] { request, cancellationToken });
        if (result is not  Task<TResponse> task)
            throw new InvalidOperationException("HandlerAsync method did not return a Task<TResponse>.");

        return await task;

    }
}