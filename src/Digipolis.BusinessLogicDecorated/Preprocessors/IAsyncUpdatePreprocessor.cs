using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IAsyncUpdatePreprocessor<TEntity> : IAsyncUpdatePreprocessor<TEntity, object>
    {
    }

    public interface IAsyncUpdatePreprocessor<TEntity, TInput>
    {
        Task PreprocessForUpdate(ref TEntity entity, ref TInput input);
    }
}
