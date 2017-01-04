using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncAddPostprocessingDecorator<TEntity> : AsyncAddPostprocessingDecorator<TEntity, object>, IAsyncAddOperator<TEntity>
    {
        public AsyncAddPostprocessingDecorator(IAsyncAddOperator<TEntity, object> addOperator, IAddPostprocessor<TEntity, object> postprocessor) : base(addOperator, postprocessor)
        {
        }

        public AsyncAddPostprocessingDecorator(IAsyncAddOperator<TEntity, object> addOperator, IAsyncAddPostprocessor<TEntity, object> postprocessor) : base(addOperator, postprocessor)
        {
        }
    }

    public class AsyncAddPostprocessingDecorator<TEntity, TInput> : AsyncAddDecorator<TEntity, TInput>
    {
        public AsyncAddPostprocessingDecorator(IAsyncAddOperator<TEntity, TInput> addOperator, IAddPostprocessor<TEntity, TInput> postprocessor) : base(addOperator)
        {
            Postprocessor = postprocessor;
        }

        public AsyncAddPostprocessingDecorator(IAsyncAddOperator<TEntity, TInput> addOperator, IAsyncAddPostprocessor<TEntity, TInput> postprocessor) : base(addOperator)
        {
            AsyncPostprocessor = postprocessor;
        }

        public IAddPostprocessor<TEntity, TInput> Postprocessor { get; set; }
        public IAsyncAddPostprocessor<TEntity, TInput> AsyncPostprocessor { get; set; }

        public override async Task<TEntity> AddAsync(TEntity entity, TInput input = default(TInput))
        {
            var result = await AddOperator.AddAsync(entity, input);

            if (Postprocessor != null)
            {
                Postprocessor.PostprocessForAdd(entity, input, ref result);
            }
            if (AsyncPostprocessor != null)
            {
                await AsyncPostprocessor.PostprocessForAdd(entity, input, ref result);
            }

            return result;
        }
    }
}
