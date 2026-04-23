using Gym.Domain.Contexts.AccountContext.Entities;

namespace Gym.Domain.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IService
{
    Task SendEmailVerificationAsync(Member member, CancellationToken cancellationToken);
}