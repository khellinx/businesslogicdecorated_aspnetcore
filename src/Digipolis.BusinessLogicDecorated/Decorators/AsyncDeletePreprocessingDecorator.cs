using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncDeletePreprocessingDecorator<TEntity> : AsyncDeletePreprocessingDecorator<TEntity, object>, IAsyncDeleteOperator<TEntity>
    {
        public AsyncDeletePreprocessingDecorator(IAsyncDeleteOperator<TEntity, object> deleteOperator, IDeletePreprocessor<TEntity, object> preprocessor) : base(deleteOperator, preprocessor)
        {
        }

        public AsyncDeletePreprocessingDecorator(IAsyncDeleteOperator<TEntity, object> deleteOperator, IAsyncDeletePreprocessor<TEntity, object> preprocessor) : base(deleteOperator, preprocessor)
        {
        }
    }

    public class AsyncDeletePreprocessingDecorator<TEntity, TInput> : AsyncDeleteDecorator<TEntity, TInput>
    {
        public AsyncDeletePreprocessingDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IDeletePreprocessor<TEntity, TInput> preprocessor) : base(deleteOperator)
        {
            Preprocessor = preprocessor;
        }

        public AsyncDeletePreprocessingDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IAsyncDeletePreprocessor<TEntity, TInput> preprocessor) : base(deleteOperator)
        {
            AsyncPreprocessor = preprocessor;
        }

        public IDeletePreprocessor<TEntity, TInput> Preprocessor { get; set; }
        public IAsyncDeletePreprocessor<TEntity, TInput> AsyncPreprocessor { get; set; }

        public override async Task DeleteAsync(int id, TInput input = default(TInput))
        {
            if (Preprocessor != null)
            {
                Preprocessor.PreprocessForDelete(id, ref input);
            }
            if (AsyncPreprocessor != null)
            {
                await AsyncPreprocessor.PreprocessForDelete(id, input);
            }

            await DeleteOperator.DeleteAsync(id, input);
        }
    }
}
