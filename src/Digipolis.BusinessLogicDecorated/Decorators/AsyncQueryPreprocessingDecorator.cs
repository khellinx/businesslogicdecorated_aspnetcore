using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.Paging;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncQueryPreprocessingDecorator<TEntity> : AsyncQueryPreprocessingDecorator<TEntity, QueryInput<TEntity>>, IAsyncQueryOperator<TEntity>
    {
        public AsyncQueryPreprocessingDecorator(IAsyncQueryOperator<TEntity, QueryInput<TEntity>> queryOperator, IQueryPreprocessor<TEntity, QueryInput<TEntity>> preprocessor) : base(queryOperator, preprocessor)
        {
        }

        public AsyncQueryPreprocessingDecorator(IAsyncQueryOperator<TEntity, QueryInput<TEntity>> queryOperator, IAsyncQueryPreprocessor<TEntity, QueryInput<TEntity>> preprocessor) : base(queryOperator, preprocessor)
        {
        }
    }

    public class AsyncQueryPreprocessingDecorator<TEntity, TInput> : AsyncQueryDecorator<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        public AsyncQueryPreprocessingDecorator(IAsyncQueryOperator<TEntity, TInput> queryOperator, IQueryPreprocessor<TEntity, TInput> preprocessor) : base(queryOperator)
        {
            Preprocessor = preprocessor;
        }

        public AsyncQueryPreprocessingDecorator(IAsyncQueryOperator<TEntity, TInput> queryOperator, IAsyncQueryPreprocessor<TEntity, TInput> preprocessor) : base(queryOperator)
        {
            AsyncPreprocessor = preprocessor;
        }

        public IQueryPreprocessor<TEntity, TInput> Preprocessor { get; set; }
        public IAsyncQueryPreprocessor<TEntity, TInput> AsyncPreprocessor { get; set; }

        public override async Task<IEnumerable<TEntity>> QueryAsync(TInput input = default(TInput))
        {
            if (Preprocessor != null)
            {
                Preprocessor.PreprocessForQuery(ref input);
            }
            if (AsyncPreprocessor != null)
            {
                await AsyncPreprocessor.PreprocessForQuery(input);
            }

            return await QueryOperator.QueryAsync(input);
        }

        public override async Task<PagedCollection<TEntity>> QueryAsync(Page page, TInput input = null)
        {
            if (Preprocessor != null)
            {
                Preprocessor.PreprocessForQuery(ref page, ref input);
            }
            if (AsyncPreprocessor != null)
            {
                await AsyncPreprocessor.PreprocessForQuery(page, input);
            }

            return await QueryOperator.QueryAsync(page, input);
        }
    }
}
