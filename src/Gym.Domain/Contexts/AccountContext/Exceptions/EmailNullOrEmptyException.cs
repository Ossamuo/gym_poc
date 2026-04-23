using Gym.Domain.Contexts.SharedContext.Exceptions;

namespace Gym.Domain.Contexts.AccountContext.Exceptions;

public class EmailNullOrEmptyException :DomainException
{
    public EmailNullOrEmptyException(string message) : base(message)
    {
        
    }

    public EmailNullOrEmptyException():base(EmailErrorMessages.NullOrEmpty)
    {
        
    }
}