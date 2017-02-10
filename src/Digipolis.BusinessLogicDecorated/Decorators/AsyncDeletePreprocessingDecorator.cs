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

    public class AsyncDeletePreprocessingDecorator<TEntity, TInput> : AsyncDeletePreprocessingDecorator<TEntity, int, TInput>, IAsyncDeleteOperator<TEntity, TInput>
    {
        public AsyncDeletePreprocessingDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IDeletePreprocessor<TEntity, TInput> preprocessor) : base(deleteOperator, preprocessor)
        {
        }

        public AsyncDeletePreprocessingDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IAsyncDeletePreprocessor<TEntity, TInput> preprocessor) : base(deleteOperator, preprocessor)
        {
        }
    }

    public class AsyncDeletePreprocessingDecorator<TEntity, TId, TInput> : AsyncDeleteDecorator<TEntity, TId, TInput>
    {
        public AsyncDeletePreprocessingDecorator(IAsyncDeleteOperator<TEntity, TId, TInput> deleteOperator, IDeletePreprocessor<TEntity, TId, TInput> preprocessor) : base(deleteOperator)
        {
            if (preprocessor == null)
            {
                throw new ArgumentNullException(nameof(preprocessor));
            }

            Preprocessor = preprocessor;
        }

        public AsyncDeletePreprocessingDecorator(IAsyncDeleteOperator<TEntity, TId, TInput> deleteOperator, IAsyncDeletePreprocessor<TEntity, TId, TInput> preprocessor) : base(deleteOperator)
        {
            if (preprocessor == null)
            {
                throw new ArgumentNullException(nameof(preprocessor));
            }

            AsyncPreprocessor = preprocessor;
        }

        public IDeletePreprocessor<TEntity, TId, TInput> Preprocessor { get; set; }
        public IAsyncDeletePreprocessor<TEntity, TId, TInput> AsyncPreprocessor { get; set; }

        public override async Task DeleteAsync(TId id, TInput input = default(TInput))
        {
            if (Preprocessor != null)
            {
                Preprocessor.PreprocessForDelete(ref id, ref input);
            }
            if (AsyncPreprocessor != null)
            {
                await AsyncPreprocessor.PreprocessForDelete(id, input);
            }

            await DeleteOperator.DeleteAsync(id, input);
        }
    }
}
