using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IAddPostprocessor<TEntity> : IAddPostprocessor<TEntity, object>
    {
    }

    public interface IAddPostprocessor<TEntity, TInput>
    {
        void PostprocessForAdd(TEntity entity, TInput input, ref TEntity result);
    }
}
