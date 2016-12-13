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

    public interface IGetPostprocessor<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        void PostprocessForGet(TInput input, ref TEntity result);
    }
}
