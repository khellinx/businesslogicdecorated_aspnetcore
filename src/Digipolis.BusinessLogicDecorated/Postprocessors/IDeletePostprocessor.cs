using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IDeletePostprocessor<TEntity> : IDeletePostprocessor<TEntity, object>
    {
    }

    public interface IDeletePostprocessor<TEntity, TInput> : IDeletePostprocessor<TEntity, int, TInput>
    {
    }

    public interface IDeletePostprocessor<TEntity, TId, TInput>
    {
        void PostprocessForDelete(TId id, TInput input);
    }
}
