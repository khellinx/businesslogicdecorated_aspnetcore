using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Inputs.Constraints
{
    public interface IHasIncludes<TEntity>
    {
        Func<IQueryable<TEntity>, IQueryable<TEntity>> Includes { get; set; }
    }
}
