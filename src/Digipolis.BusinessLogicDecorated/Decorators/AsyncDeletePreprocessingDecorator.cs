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
    }

    public class AsyncDeletePreprocessingDecorator<TEntity, TInput> : AsyncDeleteDecorator<TEntity, TInput>
    {
        public AsyncDeletePreprocessingDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IDeletePreprocessor<TEntity, TInput> preprocessor) : base(deleteOperator)
        {
            Preprocessor = preprocessor;
        }

        public IDeletePreprocessor<TEntity, TInput> Preprocessor { get; set; }

        public override Task DeleteAsync(int id, TInput input = default(TInput))
        {
            Preprocessor.PreprocessForDelete(id, ref input);

            return DeleteOperator.DeleteAsync(id, input);
        }
    }
}
