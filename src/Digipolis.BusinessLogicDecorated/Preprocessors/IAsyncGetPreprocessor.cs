using Digipolis.BusinessLogicDecorated.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IAsyncGetPreprocessor<TEntity> : IAsyncGetPreprocessor<TEntity, GetInput<TEntity>>
    {
    }

    public interface IAsyncGetPreprocessor<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        Task PreprocessForGet(ref TInput input);
    }
}
