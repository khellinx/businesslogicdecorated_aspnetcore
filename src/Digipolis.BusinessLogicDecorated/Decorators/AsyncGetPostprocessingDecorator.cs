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
    }

    public class AsyncGetPostprocessingDecorator<TEntity, TInput> : AsyncGetDecorator<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetPostprocessingDecorator(IAsyncGetOperator<TEntity, TInput> getOperator, IGetPostprocessor<TEntity, TInput> postprocessor) : base(getOperator)
        {
            Postprocessor = postprocessor;
        }

        public IGetPostprocessor<TEntity, TInput> Postprocessor { get; set; }

        public override async Task<TEntity> GetAsync(int id, TInput input = default(TInput))
        {
            var result = await GetOperator.GetAsync(id, input);

            Postprocessor.PostprocessForGet(input, ref result);

            return result;
        }
    }
}
