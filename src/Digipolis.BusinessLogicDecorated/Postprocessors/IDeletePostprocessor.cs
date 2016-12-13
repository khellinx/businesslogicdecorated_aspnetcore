using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IDeletePostprocessor<TEntity> : IDeletePostprocessor<TEntity, object>
    {
    }

    public interface IDeletePostprocessor<TEntity, TInput>
    {
        void PostprocessForDelete(int id, TInput input);
    }
}
