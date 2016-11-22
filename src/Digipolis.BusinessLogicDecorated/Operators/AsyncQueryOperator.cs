using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public class AsyncQueryOperator<TEntity, TInput> : IAsyncQueryOperator<TEntity, TInput>
        where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
    {
        public virtual Task<IEnumerable<TEntity>> QueryAsync(TInput input = null)
        {
            throw new NotImplementedException();
        }
    }
}
