using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncUpdatePreprocessingDecorator<TEntity> : AsyncUpdatePreprocessingDecorator<TEntity, object>, IAsyncUpdateOperator<TEntity>
    {
        public AsyncUpdatePreprocessingDecorator(IAsyncUpdateOperator<TEntity, object> updateOperator, IUpdatePreprocessor<TEntity, object> preprocessor) : base(updateOperator, preprocessor)
        {
        }

        public AsyncUpdatePreprocessingDecorator(IAsyncUpdateOperator<TEntity, object> updateOperator, IAsyncUpdatePreprocessor<TEntity, object> preprocessor) : base(updateOperator, preprocessor)
        {
        }
    }

    public class AsyncUpdatePreprocessingDecorator<TEntity, TInput> : AsyncUpdateDecorator<TEntity, TInput>
    {
        public AsyncUpdatePreprocessingDecorator(IAsyncUpdateOperator<TEntity, TInput> updateOperator, IUpdatePreprocessor<TEntity, TInput> preprocessor) : base(updateOperator)
        {
            Preprocessor = preprocessor;
        }

        public AsyncUpdatePreprocessingDecorator(IAsyncUpdateOperator<TEntity, TInput> updateOperator, IAsyncUpdatePreprocessor<TEntity, TInput> preprocessor) : base(updateOperator)
        {
            AsyncPreprocessor = preprocessor;
        }

        public IUpdatePreprocessor<TEntity, TInput> Preprocessor { get; set; }
        public IAsyncUpdatePreprocessor<TEntity, TInput> AsyncPreprocessor { get; set; }

        public override async Task<TEntity> UpdateAsync(TEntity entity, TInput input = default(TInput))
        {
            if (Preprocessor != null)
            {
                Preprocessor.PreprocessForUpdate(ref entity, ref input);
            }
            if (AsyncPreprocessor != null)
            {
                await AsyncPreprocessor.PreprocessForUpdate(ref entity, ref input);
            }

            return await UpdateOperator.UpdateAsync(entity, input);
        }
    }
}
