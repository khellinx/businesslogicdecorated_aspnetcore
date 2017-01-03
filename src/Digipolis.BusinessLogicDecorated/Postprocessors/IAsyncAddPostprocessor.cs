using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IAsyncAddPostprocessor<TEntity> : IAsyncAddPostprocessor<TEntity, object>
    {
    }

    public interface IAsyncAddPostprocessor<TEntity, TInput>
    {
        Task PostprocessForAdd(TEntity entity, TInput input, ref TEntity result);
    }
}
