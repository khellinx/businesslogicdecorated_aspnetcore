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
        void Preprocess(ref TEntity entity, TInput input = default(TInput));
    }
}
