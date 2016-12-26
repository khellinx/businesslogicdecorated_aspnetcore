using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IAsyncAddValidator<TEntity> : IAsyncAddValidator<TEntity, object>
    {
    }

    public interface IAsyncAddValidator<TEntity, TInput>
    {
        Task ValidateForAdd(TEntity entity, TInput input = default(TInput));
    }
}
