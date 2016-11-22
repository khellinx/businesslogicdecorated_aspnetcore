using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public abstract class AsyncDeleteDecorator<TEntity, TInput> : IAsyncDeleteOperator<TEntity, TInput>
        where TInput : class
    {
        public AsyncDeleteDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator)
        {
            if (deleteOperator == null)
            {
                throw new ArgumentNullException(nameof(deleteOperator));
            }

            DeleteOperator = deleteOperator;
        }

        public IAsyncDeleteOperator<TEntity, TInput> DeleteOperator { get; private set; }

        public abstract Task<TEntity> DeleteAsync(int id, TInput input = null);
    }
}
