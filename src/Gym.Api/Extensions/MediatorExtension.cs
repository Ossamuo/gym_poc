using System.Reflection;
using Gym.Application;
using Gym.Domain.Contexts.SharedContext.Abstractions;

namespace Gym.Api.Extensions;

internal static class MediatorExtension
{
    //with params Assembly[] assemblies I can use service.AddMediator(typeOf(Programs), tipeof(Application, ...)
    //instead of an array of Assembly service.AddMediator([typeOf(Programs), tipeof(Application), ....])
    public static IServiceCollection AddMediator(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {

        services.AddTransient<IMediator, Mediator>();

        //var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
        //    .SelectMany(a => a.GetTypes())
        //    .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>)));

        var handlerType = typeof(IHandler<,>);


        foreach (var assembly in assemblies)
        {
            var handlers = assembly
                .GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface)
                .SelectMany(x => x.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(ti => ti.Interface.IsGenericType && ti.Interface.GetGenericTypeDefinition() == handlerType);
            foreach (var handler in handlers)
            {
                services.AddTransient(handler.Interface, handler.Type);
            }

        }


        return services;
    }
}