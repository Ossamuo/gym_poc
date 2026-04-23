using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.SharedContext.Abstractions
{
    //Specification Pattern
    public interface ISpecification<in TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);

    }
}
