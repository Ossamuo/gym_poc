using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.SharedContext.Specifications.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.AccountContext.Specifications
{
    public class GetByIdSpecification(Guid id) : BaseSpecification<Member>(x => x.Id == id)
    {
    }
}
