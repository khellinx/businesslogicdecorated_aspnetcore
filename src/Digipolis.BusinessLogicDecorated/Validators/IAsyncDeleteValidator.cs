using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IAsyncDeleteValidator<TEntity> : IAsyncDeleteValidator<TEntity, object>
    {
    }

    public interface IAsyncDeleteValidator<TEntity, TInput> : IAsyncDeleteValidator<TEntity, int, TInput>
    {
    }

    public interface IAsyncDeleteValidator<TEntity, TId, TInput>
    {
        Task ValidateForDelete(TId id, TInput input = default(TInput));
    }
}
