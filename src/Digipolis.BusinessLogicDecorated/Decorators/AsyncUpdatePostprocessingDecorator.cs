using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncUpdatePostprocessingDecorator<TEntity> : AsyncUpdatePostprocessingDecorator<TEntity, object>, IAsyncUpdateOperator<TEntity>
    {
        public AsyncUpdatePostprocessingDecorator(IAsyncUpdateOperator<TEntity, object> updateOperator, IUpdatePostprocessor<TEntity, object> postprocessor) : base(updateOperator, postprocessor)
        {
        }
    }

    public class AsyncUpdatePostprocessingDecorator<TEntity, TInput> : AsyncUpdateDecorator<TEntity, TInput>
    {
        public AsyncUpdatePostprocessingDecorator(IAsyncUpdateOperator<TEntity, TInput> updateOperator, IUpdatePostprocessor<TEntity, TInput> postprocessor) : base(updateOperator)
        {
            Postprocessor = postprocessor;
        }

        public IUpdatePostprocessor<TEntity, TInput> Postprocessor { get; set; }

        public override async Task<TEntity> UpdateAsync(TEntity entity, TInput input = default(TInput))
        {
            var result = await UpdateOperator.UpdateAsync(entity, input);

            Postprocessor.PostprocessForUpdate(entity, input, ref result);

            return result;
        }
    }
}
