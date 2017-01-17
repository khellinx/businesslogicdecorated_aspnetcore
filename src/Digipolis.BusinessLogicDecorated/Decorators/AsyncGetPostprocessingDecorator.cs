using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncGetPostprocessingDecorator<TEntity> : AsyncGetPostprocessingDecorator<TEntity, GetInput<TEntity>>, IAsyncGetOperator<TEntity>
    {
        public AsyncGetPostprocessingDecorator(IAsyncGetOperator<TEntity> getOperator, IGetPostprocessor<TEntity> postprocessor) : base(getOperator, postprocessor)
        {
        }

        public AsyncGetPostprocessingDecorator(IAsyncGetOperator<TEntity> getOperator, IAsyncGetPostprocessor<TEntity> postprocessor) : base(getOperator, postprocessor)
        {
        }
    }

    public class AsyncGetPostprocessingDecorator<TEntity, TInput> : AsyncGetPostprocessingDecorator<TEntity, int, TInput>, IAsyncGetOperator<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetPostprocessingDecorator(IAsyncGetOperator<TEntity, TInput> getOperator, IGetPostprocessor<TEntity, TInput> postprocessor) : base(getOperator, postprocessor)
        {
        }

        public AsyncGetPostprocessingDecorator(IAsyncGetOperator<TEntity, TInput> getOperator, IAsyncGetPostprocessor<TEntity, TInput> postprocessor) : base(getOperator, postprocessor)
        {
        }
    }

    public class AsyncGetPostprocessingDecorator<TEntity, TId, TInput> : AsyncGetDecorator<TEntity, TId, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetPostprocessingDecorator(IAsyncGetOperator<TEntity, TId, TInput> getOperator, IGetPostprocessor<TEntity, TId, TInput> postprocessor) : base(getOperator)
        {
            Postprocessor = postprocessor;
        }

        public AsyncGetPostprocessingDecorator(IAsyncGetOperator<TEntity, TId, TInput> getOperator, IAsyncGetPostprocessor<TEntity, TId, TInput> postprocessor) : base(getOperator)
        {
            AsyncPostprocessor = postprocessor;
        }

        public IGetPostprocessor<TEntity, TId, TInput> Postprocessor { get; set; }
        public IAsyncGetPostprocessor<TEntity, TId, TInput> AsyncPostprocessor { get; set; }

        public override async Task<TEntity> GetAsync(TId id, TInput input = default(TInput))
        {
            var result = await GetOperator.GetAsync(id, input);

            if (Postprocessor != null)
            {
                Postprocessor.PostprocessForGet(id, input, ref result);
            }
            if (AsyncPostprocessor != null)
            {
                await AsyncPostprocessor.PostprocessForGet(id, input, result);
            }

            return result;
        }
    }
}
