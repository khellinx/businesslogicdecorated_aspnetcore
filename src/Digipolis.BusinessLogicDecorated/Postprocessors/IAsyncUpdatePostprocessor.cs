using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IAsyncUpdatePostprocessor<TEntity> : IAsyncUpdatePostprocessor<TEntity, object>
    {
    }

    public interface IAsyncUpdatePostprocessor<TEntity, TInput>
    {
        Task PostprocessForUpdate(TEntity entity, TInput input, ref TEntity result);
    }
}
