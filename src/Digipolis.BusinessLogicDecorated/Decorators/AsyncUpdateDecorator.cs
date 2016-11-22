using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public abstract class AsyncUpdateDecorator<TEntity, TInput> : IAsyncUpdateOperator<TEntity, TInput>
        where TInput : class
    {
        public AsyncUpdateDecorator(IAsyncUpdateOperator<TEntity, TInput> updateOperator)
        {
            if (updateOperator == null)
            {
                throw new ArgumentNullException(nameof(updateOperator));
            }

            UpdateOperator = updateOperator;
        }

        public IAsyncUpdateOperator<TEntity, TInput> UpdateOperator { get; private set; }

        public abstract Task<TEntity> UpdateAsync(TEntity entity, TInput input = null);
    }
}
