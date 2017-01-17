using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IAsyncDeletePreprocessor<TEntity> : IAsyncDeletePreprocessor<TEntity, object>
    {
    }

    public interface IAsyncDeletePreprocessor<TEntity, TInput> : IAsyncDeletePreprocessor<TEntity, int, TInput>
    {
    }

    public interface IAsyncDeletePreprocessor<TEntity, TId, TInput>
    {
        Task PreprocessForDelete(TId id, TInput input);
    }
}
