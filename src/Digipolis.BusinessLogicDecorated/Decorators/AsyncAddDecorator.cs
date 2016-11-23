using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public abstract class AsyncAddDecorator<TEntity, TInput> : IAsyncAddOperator<TEntity, TInput>
    {
        public AsyncAddDecorator(IAsyncAddOperator<TEntity, TInput> addOperator)
        {
            if (addOperator == null)
            {
                throw new ArgumentNullException(nameof(addOperator));
            }

            AddOperator = addOperator;
        }

        public IAsyncAddOperator<TEntity, TInput> AddOperator { get; private set; }

        public abstract Task<TEntity> AddAsync(TEntity entity, TInput input = default(TInput));
    }
}
