using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Inputs.Constraints
{
    public interface IHasOrder<TEntity>
    {
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> Order { get; set; }
    }
}
