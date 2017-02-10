using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncGetPreprocessingDecorator<TEntity> : AsyncGetPreprocessingDecorator<TEntity, GetInput<TEntity>>, IAsyncGetOperator<TEntity>
    {
        public AsyncGetPreprocessingDecorator(IAsyncGetOperator<TEntity> getOperator, IGetPreprocessor<TEntity> preprocessor) : base(getOperator, preprocessor)
        {
        }

        public AsyncGetPreprocessingDecorator(IAsyncGetOperator<TEntity> getOperator, IAsyncGetPreprocessor<TEntity> preprocessor) : base(getOperator, preprocessor)
        {
        }
    }

    public class AsyncGetPreprocessingDecorator<TEntity, TInput> : AsyncGetPreprocessingDecorator<TEntity, int, TInput>, IAsyncGetOperator<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetPreprocessingDecorator(IAsyncGetOperator<TEntity, TInput> getOperator, IGetPreprocessor<TEntity, TInput> preprocessor) : base(getOperator, preprocessor)
        {
        }

        public AsyncGetPreprocessingDecorator(IAsyncGetOperator<TEntity, TInput> getOperator, IAsyncGetPreprocessor<TEntity, TInput> preprocessor) : base(getOperator, preprocessor)
        {
        }
    }

    public class AsyncGetPreprocessingDecorator<TEntity, TId, TInput> : AsyncGetDecorator<TEntity, TId, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetPreprocessingDecorator(IAsyncGetOperator<TEntity, TId, TInput> getOperator, IGetPreprocessor<TEntity, TId, TInput> preprocessor) : base(getOperator)
        {
            if (preprocessor == null)
            {
                throw new ArgumentNullException(nameof(preprocessor));
            }

            Preprocessor = preprocessor;
        }

        public AsyncGetPreprocessingDecorator(IAsyncGetOperator<TEntity, TId, TInput> getOperator, IAsyncGetPreprocessor<TEntity, TId, TInput> preprocessor) : base(getOperator)
        {
            if (preprocessor == null)
            {
                throw new ArgumentNullException(nameof(preprocessor));
            }

            AsyncPreprocessor = preprocessor;
        }

        public IGetPreprocessor<TEntity, TId, TInput> Preprocessor { get; set; }
        public IAsyncGetPreprocessor<TEntity, TId, TInput> AsyncPreprocessor { get; set; }

        public override async Task<TEntity> GetAsync(TId id, TInput input = default(TInput))
        {
            if (Preprocessor != null)
            {
                Preprocessor.PreprocessForGet(ref id, ref input);
            }
            if (AsyncPreprocessor != null)
            {
                await AsyncPreprocessor.PreprocessForGet(id, input);
            }

            return await GetOperator.GetAsync(id, input);
        }
    }
}
