using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IAsyncUpdateValidator<TEntity> : IAsyncUpdateValidator<TEntity, object>
    {
    }

    public interface IAsyncUpdateValidator<TEntity, TInput>
    {
        Task ValidateForUpdate(TEntity entity, TInput input = default(TInput));
    }
}
