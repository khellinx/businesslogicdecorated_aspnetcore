using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public interface IAsyncQueryOperator<TEntity> : IAsyncQueryOperator<TEntity, QueryInput<TEntity>>
    {
    }

    public interface IAsyncQueryOperator<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        Task<IEnumerable<TEntity>> QueryAsync(TInput input = default(TInput));
        Task<PagedCollection<TEntity>> QueryAsync(Page page, TInput input = default(TInput));
    }
}
