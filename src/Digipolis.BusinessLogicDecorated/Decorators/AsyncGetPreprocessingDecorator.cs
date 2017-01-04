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

    public class AsyncGetPreprocessingDecorator<TEntity, TInput> : AsyncGetDecorator<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetPreprocessingDecorator(IAsyncGetOperator<TEntity, TInput> getOperator, IGetPreprocessor<TEntity, TInput> preprocessor) : base(getOperator)
        {
            Preprocessor = preprocessor;
        }

        public AsyncGetPreprocessingDecorator(IAsyncGetOperator<TEntity, TInput> getOperator, IAsyncGetPreprocessor<TEntity, TInput> preprocessor) : base(getOperator)
        {
            AsyncPreprocessor = preprocessor;
        }

        public IGetPreprocessor<TEntity, TInput> Preprocessor { get; set; }
        public IAsyncGetPreprocessor<TEntity, TInput> AsyncPreprocessor { get; set; }

        public override async Task<TEntity> GetAsync(int id, TInput input = default(TInput))
        {
            if (Preprocessor != null)
            {
                Preprocessor.PreprocessForGet(ref input);
            }
            if (AsyncPreprocessor != null)
            {
                await AsyncPreprocessor.PreprocessForGet(input);
            }

            return await GetOperator.GetAsync(id, input);
        }
    }
}
