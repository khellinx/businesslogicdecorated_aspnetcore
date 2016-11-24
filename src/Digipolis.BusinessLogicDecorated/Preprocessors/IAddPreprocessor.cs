using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IAddPreprocessor<TEntity> : IAddPreprocessor<TEntity, object>
    {
    }

    public interface IAddPreprocessor<TEntity, TInput>
    {
        void PreprocessForAdd(ref TEntity entity, ref TInput input);
    }
}
