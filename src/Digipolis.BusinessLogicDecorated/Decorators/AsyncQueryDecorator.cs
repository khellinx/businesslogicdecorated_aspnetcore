using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public abstract class AsyncQueryDecorator<TEntity, TInput> : IAsyncQueryOperator<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        public AsyncQueryDecorator(IAsyncQueryOperator<TEntity, TInput> queryOperator)
        {
            QueryOperator = queryOperator;
        }

        public IAsyncQueryOperator<TEntity, TInput> QueryOperator { get; private set; }

        public abstract Task<IEnumerable<TEntity>> QueryAsync(TInput input = default(TInput));
        public abstract Task<PagedCollection<TEntity>> QueryAsync(Page page, TInput input = default(TInput));
    }
}
