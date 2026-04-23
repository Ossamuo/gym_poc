using Gym.Domain.Contexts.PartnerContext.Entities;
using Gym.Domain.Contexts.PartnerContext.UseCases.Auth.Contracts;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Contexts.PartnerContext.UseCases.Auth;

public class Repository(AppDbContext context) : IRepository
{
    public Task<Partner?> GetByApiKeyAsync(string apiKey, CancellationToken cancellationToken)
        => context.Partners
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ApiKey == apiKey, cancellationToken);
}
