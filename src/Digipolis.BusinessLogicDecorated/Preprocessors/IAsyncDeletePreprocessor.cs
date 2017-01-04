using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IAsyncDeletePreprocessor<TEntity> : IAsyncDeletePreprocessor<TEntity, object>
    {
    }

    public interface IAsyncDeletePreprocessor<TEntity, TInput>
    {
        Task PreprocessForDelete(int id, TInput input);
    }
}
