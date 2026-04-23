using Gym.Domain.Contexts.PartnerContext.Entities;

namespace Gym.Domain.Contexts.PartnerContext.UseCases.Auth.Contracts;

public interface IRepository
{
    Task<Partner?> GetByApiKeyAsync(string apiKey, CancellationToken cancellationToken);
}
