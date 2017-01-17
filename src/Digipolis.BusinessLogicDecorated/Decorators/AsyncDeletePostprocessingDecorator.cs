using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncDeletePostprocessingDecorator<TEntity> : AsyncDeletePostprocessingDecorator<TEntity, object>, IAsyncDeleteOperator<TEntity>
    {
        public AsyncDeletePostprocessingDecorator(IAsyncDeleteOperator<TEntity, object> deleteOperator, IDeletePostprocessor<TEntity, object> postprocessor) : base(deleteOperator, postprocessor)
        {
        }

        public AsyncDeletePostprocessingDecorator(IAsyncDeleteOperator<TEntity, object> deleteOperator, IAsyncDeletePostprocessor<TEntity, object> postprocessor) : base(deleteOperator, postprocessor)
        {
        }
    }

    public class AsyncDeletePostprocessingDecorator<TEntity, TInput> : AsyncDeletePostprocessingDecorator<TEntity, int, TInput>, IAsyncDeleteOperator<TEntity, TInput>
    {
        public AsyncDeletePostprocessingDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IDeletePostprocessor<TEntity, TInput> postprocessor) : base(deleteOperator, postprocessor)
        {
        }

        public AsyncDeletePostprocessingDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IAsyncDeletePostprocessor<TEntity, TInput> postprocessor) : base(deleteOperator, postprocessor)
        {
        }
    }

    public class AsyncDeletePostprocessingDecorator<TEntity, TId, TInput> : AsyncDeleteDecorator<TEntity, TId, TInput>
    {
        public AsyncDeletePostprocessingDecorator(IAsyncDeleteOperator<TEntity, TId, TInput> deleteOperator, IDeletePostprocessor<TEntity, TId, TInput> postprocessor) : base(deleteOperator)
        {
            Postprocessor = postprocessor;
        }

        public AsyncDeletePostprocessingDecorator(IAsyncDeleteOperator<TEntity, TId, TInput> deleteOperator, IAsyncDeletePostprocessor<TEntity, TId, TInput> postprocessor) : base(deleteOperator)
        {
            AsyncPostprocessor = postprocessor;
        }

        public IDeletePostprocessor<TEntity, TId, TInput> Postprocessor { get; set; }
        public IAsyncDeletePostprocessor<TEntity, TId, TInput> AsyncPostprocessor { get; set; }

        public override async Task DeleteAsync(TId id, TInput input = default(TInput))
        {
            await DeleteOperator.DeleteAsync(id, input);

            if (Postprocessor != null)
            {
                Postprocessor.PostprocessForDelete(id, input);
            }
            if (AsyncPostprocessor != null)
            {
                await AsyncPostprocessor.PostprocessForDelete(id, input);
            }
        }
    }
}
