using Gym.Domain.Contexts.AccountContext.Entities;

namespace Gym.Domain.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(string requestName, string email, CancellationToken cancellationToken);
    Task SaveAsync(Member member, CancellationToken cancellationToken);
}