using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;

namespace Gym.Domain.Contexts.AccountContext.UseCases.Create;

public class Request : IRequest<Result<Response?>>
{
    public Request()
    {
            
    }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}