using Gym.Domain.Contexts.SharedContext.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.SharedContext.Specifications.Abstractions
{
    //This is the base specification class.
    //all other class must inherit this class in order to use as a query expression 
    //study a way if is possible using  only interface 
    public abstract class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; } = criteria;

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = Criteria.Compile();
            return predicate(entity);
        }
    }
}
