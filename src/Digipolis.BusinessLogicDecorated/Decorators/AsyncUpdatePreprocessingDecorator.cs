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
    }

    public class AsyncUpdatePreprocessingDecorator<TEntity, TInput> : AsyncUpdateDecorator<TEntity, TInput>
    {
        public AsyncUpdatePreprocessingDecorator(IAsyncUpdateOperator<TEntity, TInput> updateOperator, IUpdatePreprocessor<TEntity, TInput> preprocessor) : base(updateOperator)
        {
            Preprocessor = preprocessor;
        }

        public IUpdatePreprocessor<TEntity, TInput> Preprocessor { get; set; }

        public override Task<TEntity> UpdateAsync(TEntity entity, TInput input = default(TInput))
        {
            Preprocessor.PreprocessForUpdate(ref entity, ref input);

            return UpdateOperator.UpdateAsync(entity, input);
        }
    }
}
