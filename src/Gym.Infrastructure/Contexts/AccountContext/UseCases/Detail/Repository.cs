using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.Specifications;
using Gym.Domain.Contexts.AccountContext.UseCases.Detail.Contracts;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Contexts.AccountContext.UseCases.Detail
{
    public class Repository(AppDbContext context) : IRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Member?> GetByIdAsycn(GetByIdSpecification specification)
        {
            var member = await _context.Members
                .AsNoTracking()
                .Where(specification.Criteria)
                .FirstOrDefaultAsync();

            return member;
        }
    }
}
