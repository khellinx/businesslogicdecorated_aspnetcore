using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public abstract class AsyncGetDecorator<TEntity, TInput> : IAsyncGetOperator<TEntity, TInput>
        where TInput : IHasIncludes<TEntity>
    {
        public AsyncGetDecorator(IAsyncGetOperator<TEntity, TInput> getOperator)
        {
            if (getOperator == null)
            {
                throw new ArgumentNullException(nameof(getOperator));
            }

            GetOperator = getOperator;
        }

        public IAsyncGetOperator<TEntity, TInput> GetOperator { get; private set; }

        public abstract Task<TEntity> GetAsync(int id, TInput input = default(TInput));
    }
}
