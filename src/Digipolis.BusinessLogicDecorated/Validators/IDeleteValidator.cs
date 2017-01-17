using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IDeleteValidator<TEntity> : IDeleteValidator<TEntity, object>
    {
    }

    public interface IDeleteValidator<TEntity, TInput> : IDeleteValidator<TEntity, int, TInput>
    {
    }

    public interface IDeleteValidator<TEntity, TId, TInput>
    {
        void ValidateForDelete(TId id, TInput input = default(TInput));
    }
}
