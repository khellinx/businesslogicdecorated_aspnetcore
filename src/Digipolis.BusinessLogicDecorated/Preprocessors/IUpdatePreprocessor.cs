using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IUpdatePreprocessor<TEntity> : IUpdatePreprocessor<TEntity, object>
    {
    }

    public interface IUpdatePreprocessor<TEntity, TInput>
    {
        void PreprocessForUpdate(ref TEntity entity, ref TInput input);
    }
}
