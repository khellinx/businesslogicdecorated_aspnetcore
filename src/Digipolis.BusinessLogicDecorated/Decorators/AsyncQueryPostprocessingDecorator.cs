using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Paging;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncQueryPostprocessingDecorator<TEntity> : AsyncQueryPostprocessingDecorator<TEntity, QueryInput<TEntity>>, IAsyncQueryOperator<TEntity>
    {
        public AsyncQueryPostprocessingDecorator(IAsyncQueryOperator<TEntity, QueryInput<TEntity>> queryOperator, IQueryPostprocessor<TEntity, QueryInput<TEntity>> postprocessor) : base(queryOperator, postprocessor)
        {
        }
    }

    public class AsyncQueryPostprocessingDecorator<TEntity, TInput> : AsyncQueryDecorator<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        public AsyncQueryPostprocessingDecorator(IAsyncQueryOperator<TEntity, TInput> queryOperator, IQueryPostprocessor<TEntity, TInput> postprocessor) : base(queryOperator)
        {
            Postprocessor = postprocessor;
        }

        public IQueryPostprocessor<TEntity, TInput> Postprocessor { get; set; }

        public override async Task<IEnumerable<TEntity>> QueryAsync(TInput input = default(TInput))
        {
            var result = await QueryOperator.QueryAsync(input);

            Postprocessor.PostprocessForQuery(input, ref result);

            return result;
        }

        public override async Task<PagedCollection<TEntity>> QueryAsync(Page page, TInput input = null)
        {
            var result = await QueryOperator.QueryAsync(page, input);

            Postprocessor.PostprocessForQuery(page, input, ref result);

            return result;
        }
    }
}
