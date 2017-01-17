using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IDeletePreprocessor<TEntity> : IDeletePreprocessor<TEntity, object>
    {
    }

    public interface IDeletePreprocessor<TEntity, TInput> : IDeletePreprocessor<TEntity, int, TInput>
    {
    }

    public interface IDeletePreprocessor<TEntity, TId, TInput>
    {
        void PreprocessForDelete(ref TId id, ref TInput input);
    }
}
