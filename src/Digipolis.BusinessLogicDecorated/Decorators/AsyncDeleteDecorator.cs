using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public abstract class AsyncDeleteDecorator<TEntity, TId, TInput> : IAsyncDeleteOperator<TEntity, TId, TInput>
    {
        public AsyncDeleteDecorator(IAsyncDeleteOperator<TEntity, TId, TInput> deleteOperator)
        {
            if (deleteOperator == null)
            {
                throw new ArgumentNullException(nameof(deleteOperator));
            }

            DeleteOperator = deleteOperator;
        }

        public IAsyncDeleteOperator<TEntity, TId, TInput> DeleteOperator { get; private set; }

        public abstract Task DeleteAsync(TId id, TInput input = default(TInput));
    }
}
