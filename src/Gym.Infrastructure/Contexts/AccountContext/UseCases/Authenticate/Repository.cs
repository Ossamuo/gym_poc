using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Contexts.AccountContext.UseCases.Authenticate;

public class Repository (AppDbContext dbContext): IRepository
{
    private readonly AppDbContext _appDbContext = dbContext;
    public async Task<Member?> GetMemberByEmailAsync(string requestEmail, CancellationToken cancellationToken)
    {
        return await _appDbContext
            .Members
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(member => member.Email.Address == requestEmail, cancellationToken);
    }
}