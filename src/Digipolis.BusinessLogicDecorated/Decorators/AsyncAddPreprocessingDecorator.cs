using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncAddPreprocessingDecorator<TEntity> : AsyncAddPreprocessingDecorator<TEntity, object>, IAsyncAddOperator<TEntity>
    {
        public AsyncAddPreprocessingDecorator(IAsyncAddOperator<TEntity, object> addOperator, IAddPreprocessor<TEntity, object> preprocessor) : base(addOperator, preprocessor)
        {
        }

        public AsyncAddPreprocessingDecorator(IAsyncAddOperator<TEntity, object> addOperator, IAsyncAddPreprocessor<TEntity, object> preprocessor) : base(addOperator, preprocessor)
        {
        }
    }

    public class AsyncAddPreprocessingDecorator<TEntity, TInput> : AsyncAddDecorator<TEntity, TInput>
    {
        public AsyncAddPreprocessingDecorator(IAsyncAddOperator<TEntity, TInput> addOperator, IAddPreprocessor<TEntity, TInput> preprocessor) : base(addOperator)
        {
            if (preprocessor == null)
            {
                throw new ArgumentNullException(nameof(preprocessor));
            }

            Preprocessor = preprocessor;
        }

        public AsyncAddPreprocessingDecorator(IAsyncAddOperator<TEntity, TInput> addOperator, IAsyncAddPreprocessor<TEntity, TInput> preprocessor) : base(addOperator)
        {
            if (preprocessor == null)
            {
                throw new ArgumentNullException(nameof(preprocessor));
            }

            AsyncPreprocessor = preprocessor;
        }

        public IAddPreprocessor<TEntity, TInput> Preprocessor { get; set; }
        public IAsyncAddPreprocessor<TEntity, TInput> AsyncPreprocessor { get; set; }

        public override async Task<TEntity> AddAsync(TEntity entity, TInput input = default(TInput))
        {
            if (Preprocessor != null)
            {
                Preprocessor.PreprocessForAdd(ref entity, ref input);
            }
            if (AsyncPreprocessor != null)
            {
                await AsyncPreprocessor.PreprocessForAdd(entity, input);
            }

            return await AddOperator.AddAsync(entity, input);
        }
    }
}
