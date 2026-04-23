using Gym.Domain.Contexts.AccountContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.AccountContext.UseCases.Edit.Contracts
{
    public interface IRepository
    {
        Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Request request, CancellationToken cancellationToken);
    }
}
