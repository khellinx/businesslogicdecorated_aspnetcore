using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IQueryPostprocessor<TEntity> : IQueryPostprocessor<TEntity, QueryInput<TEntity>>
    {
    }

    public interface IQueryPostprocessor<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        void PostprocessForQuery(TInput input, ref IEnumerable<TEntity> result);
        void PostprocessForQuery(Page page, TInput input, ref PagedCollection<TEntity> result);
    }
}
