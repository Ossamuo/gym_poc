using Gym.Domain.Contexts.AccountContext.Entities;

namespace Gym.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;

public interface IRepository
{
    Task<Member?> GetMemberByEmailAsync(string requestEmail, CancellationToken cancellationToken);
}