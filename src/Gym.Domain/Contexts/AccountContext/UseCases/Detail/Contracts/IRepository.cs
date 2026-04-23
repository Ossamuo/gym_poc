using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.AccountContext.UseCases.Detail.Contracts
{
    public interface IRepository
    {
        Task<Member?> GetByIdAsycn(GetByIdSpecification specification);
    }
}
