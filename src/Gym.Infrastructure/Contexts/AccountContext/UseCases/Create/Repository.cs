using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.UseCases.Create.Contracts;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Contexts.AccountContext.UseCases.Create;

public class Repository(AppDbContext context) : IRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> AnyAsync(string requestName, string email, CancellationToken cancellationToken)
    {
        return await _context
            .Members
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email, cancellationToken);
    }

    public async Task SaveAsync(Member member, CancellationToken cancellationToken)
    {
        await _context.Members.AddAsync(member, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}