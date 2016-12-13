using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncDeletePostprocessingDecorator
    {
    }

    public class AsyncDeletePostprocessingDecorator<TEntity> : AsyncDeletePostprocessingDecorator<TEntity, object>, IAsyncDeleteOperator<TEntity>
    {
        public AsyncDeletePostprocessingDecorator(IAsyncDeleteOperator<TEntity, object> deleteOperator, IDeletePostprocessor<TEntity, object> postprocessor) : base(deleteOperator, postprocessor)
        {
        }
    }

    public class AsyncDeletePostprocessingDecorator<TEntity, TInput> : AsyncDeleteDecorator<TEntity, TInput>
    {
        public AsyncDeletePostprocessingDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IDeletePostprocessor<TEntity, TInput> postprocessor) : base(deleteOperator)
        {
            Postprocessor = postprocessor;
        }

        public IDeletePostprocessor<TEntity, TInput> Postprocessor { get; set; }

        public override async Task DeleteAsync(int id, TInput input = default(TInput))
        {
            await DeleteOperator.DeleteAsync(id, input);

            Postprocessor.PostprocessForDelete(id, input);
        }
    }
}
