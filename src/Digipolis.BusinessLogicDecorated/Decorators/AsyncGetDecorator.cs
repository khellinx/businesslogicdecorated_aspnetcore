using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public abstract class AsyncGetDecorator<TEntity, TId, TInput> : IAsyncGetOperator<TEntity, TId, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetDecorator(IAsyncGetOperator<TEntity, TId, TInput> getOperator)
        {
            if (getOperator == null)
            {
                throw new ArgumentNullException(nameof(getOperator));
            }

            GetOperator = getOperator;
        }

        public IAsyncGetOperator<TEntity, TId, TInput> GetOperator { get; private set; }

        public abstract Task<TEntity> GetAsync(TId id, TInput input = default(TInput));
    }
}
