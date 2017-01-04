using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IAsyncQueryPostprocessor<TEntity> : IAsyncQueryPostprocessor<TEntity, QueryInput<TEntity>>
    {
    }

    public interface IAsyncQueryPostprocessor<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        Task PostprocessForQuery(TInput input, IEnumerable<TEntity> result);
        Task PostprocessForQuery(Page page, TInput input, PagedCollection<TEntity> result);
    }
}
