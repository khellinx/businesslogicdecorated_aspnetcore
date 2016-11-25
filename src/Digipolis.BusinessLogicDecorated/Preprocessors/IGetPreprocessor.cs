using Digipolis.BusinessLogicDecorated.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IGetPreprocessor<TEntity> : IGetPreprocessor<TEntity, GetInput<TEntity>>
    {
    }

    public interface IGetPreprocessor<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        void PreprocessForGet(ref TInput input);
    }
}
