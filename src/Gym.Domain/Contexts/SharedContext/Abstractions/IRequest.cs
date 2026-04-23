namespace Gym.Domain.Contexts.SharedContext.Abstractions;
/// <summary>
/// the interface for typing all que request used by the Handlers
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IRequest<TResponse>
{
    //Do not constrain the  TResponse e.g  `where TResponse: IResponse or class`
    //if the user want to return a primitive type like int, string, bool etc. it would not be allowed

}