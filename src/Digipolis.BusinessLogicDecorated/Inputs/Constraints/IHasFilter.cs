using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Inputs.Constraints
{
    public interface IHasFilter<TEntity>
    {
        Expression<Func<TEntity, bool>> Filter { get; set; }
    }
}
