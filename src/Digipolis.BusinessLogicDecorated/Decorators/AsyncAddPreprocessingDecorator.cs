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
    }

    public class AsyncAddPreprocessingDecorator<TEntity, TInput> : AsyncAddDecorator<TEntity, TInput>
    {
        public AsyncAddPreprocessingDecorator(IAsyncAddOperator<TEntity, TInput> addOperator, IAddPreprocessor<TEntity, TInput> preprocessor) : base(addOperator)
        {
            Preprocessor = preprocessor;
        }

        public IAddPreprocessor<TEntity, TInput> Preprocessor { get; set; }

        public override Task<TEntity> AddAsync(TEntity entity, TInput input = default(TInput))
        {
            Preprocessor.PreprocessForAdd(ref entity, ref input);

            return AddOperator.AddAsync(entity, input);
        }
    }
}
