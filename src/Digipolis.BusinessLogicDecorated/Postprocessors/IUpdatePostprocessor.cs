using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IUpdatePostprocessor<TEntity> : IUpdatePostprocessor<TEntity, object>
    {
    }

    public interface IUpdatePostprocessor<TEntity, TInput>
    {
        void PostprocessForUpdate(TEntity entity, TInput input, ref TEntity result);
    }
}
