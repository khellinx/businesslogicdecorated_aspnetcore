using Digipolis.BusinessLogicDecorated.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IAsyncGetPostprocessor<TEntity> : IAsyncGetPostprocessor<TEntity, GetInput<TEntity>>
    {
    }

    public interface IAsyncGetPostprocessor<TEntity, TInput> : IAsyncGetPostprocessor<TEntity, int, TInput>
        where TInput : GetInput<TEntity>
    {
    }

    public interface IAsyncGetPostprocessor<TEntity, TId, TInput>
        where TInput : GetInput<TEntity>
    {
        Task PostprocessForGet(TId id, TInput input, TEntity result);
    }
}
