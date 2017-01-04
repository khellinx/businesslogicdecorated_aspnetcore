using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IAsyncAddPreprocessor<TEntity> : IAsyncAddPreprocessor<TEntity, object>
    {
    }

    public interface IAsyncAddPreprocessor<TEntity, TInput>
    {
        Task PreprocessForAdd(TEntity entity, TInput input);
    }
}
