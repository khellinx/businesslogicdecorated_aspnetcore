using Digipolis.BusinessLogicDecorated.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IGetPostprocessor<TEntity> : IGetPostprocessor<TEntity, GetInput<TEntity>>
    {
    }

    public interface IGetPostprocessor<TEntity, TInput> : IGetPostprocessor<TEntity, int, TInput>
        where TInput : GetInput<TEntity>
    {
    }

    public interface IGetPostprocessor<TEntity, TId, TInput>
        where TInput : GetInput<TEntity>
    {
        void PostprocessForGet(TId id, TInput input, ref TEntity result);
    }
}
