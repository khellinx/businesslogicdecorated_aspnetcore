using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public class AsyncGetOperator<TEntity, TInput> : IAsyncGetOperator<TEntity, TInput>
        where TInput : IHasIncludes<TEntity>
    {
        public virtual Task<TEntity> GetAsync(int id, TInput input = default(TInput))
        {
            throw new NotImplementedException();
        }
    }
}
