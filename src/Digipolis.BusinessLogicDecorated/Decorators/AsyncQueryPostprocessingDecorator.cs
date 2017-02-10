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

        public AsyncQueryPostprocessingDecorator(IAsyncQueryOperator<TEntity, QueryInput<TEntity>> queryOperator, IAsyncQueryPostprocessor<TEntity, QueryInput<TEntity>> postprocessor) : base(queryOperator, postprocessor)
        {
        }
    }

    public class AsyncQueryPostprocessingDecorator<TEntity, TInput> : AsyncQueryDecorator<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        public AsyncQueryPostprocessingDecorator(IAsyncQueryOperator<TEntity, TInput> queryOperator, IQueryPostprocessor<TEntity, TInput> postprocessor) : base(queryOperator)
        {
            if (postprocessor == null)
            {
                throw new ArgumentNullException(nameof(postprocessor));
            }

            Postprocessor = postprocessor;
        }

        public AsyncQueryPostprocessingDecorator(IAsyncQueryOperator<TEntity, TInput> queryOperator, IAsyncQueryPostprocessor<TEntity, TInput> postprocessor) : base(queryOperator)
        {
            if (postprocessor == null)
            {
                throw new ArgumentNullException(nameof(postprocessor));
            }

            AsyncPostprocessor = postprocessor;
        }

        public IQueryPostprocessor<TEntity, TInput> Postprocessor { get; set; }
        public IAsyncQueryPostprocessor<TEntity, TInput> AsyncPostprocessor { get; set; }

        public override async Task<IEnumerable<TEntity>> QueryAsync(TInput input = default(TInput))
        {
            var result = await QueryOperator.QueryAsync(input);

            if (Postprocessor != null)
            {
                Postprocessor.PostprocessForQuery(input, ref result);
            }
            if (AsyncPostprocessor != null)
            {
                await AsyncPostprocessor.PostprocessForQuery(input, result);
            }

            return result;
        }

        public override async Task<PagedCollection<TEntity>> QueryAsync(Page page, TInput input = null)
        {
            var result = await QueryOperator.QueryAsync(page, input);

            if (Postprocessor != null)
            {
                Postprocessor.PostprocessForQuery(page, input, ref result);
            }
            if (AsyncPostprocessor != null)
            {
                await AsyncPostprocessor.PostprocessForQuery(page, input, result);
            }

            return result;
        }
    }
}
