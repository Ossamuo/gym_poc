
using EllipticCurve.Utils;
using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.UseCases.Edit;
using Gym.Domain.Contexts.AccountContext.UseCases.Edit.Contracts;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Contexts.AccountContext.UseCases.Edit
{
    public class Repository(AppDbContext context) : IRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context
                .Members
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }


        public async Task UpdateAsync(Request request, CancellationToken cancellationToken)
        {
            var member = await _context
                .Members
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            member!.Name = request.Name;
            member!.Image = request.Image;
            context.Members.Update(member);
            await _context.SaveChangesAsync(cancellationToken);            
        }
    }
}
