using Gym.Domain.Contexts.SharedContext.Exceptions;

namespace Gym.Domain.Contexts.AccountContext.Exceptions;

public class InvalidEmailException :DomainException
{
    public InvalidEmailException(string message) : base(message)
    {
        
    }

    public InvalidEmailException():base(EmailErrorMessages.Invalid)
    {
        
    }
}