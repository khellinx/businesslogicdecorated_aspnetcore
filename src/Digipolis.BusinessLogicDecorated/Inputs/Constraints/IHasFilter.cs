using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Inputs.Constraints
{
    public interface IHasFilter<TEntity>
    {
        Func<IQueryable<TEntity>, IQueryable<TEntity>> Filter { get; set; }
    }
}
