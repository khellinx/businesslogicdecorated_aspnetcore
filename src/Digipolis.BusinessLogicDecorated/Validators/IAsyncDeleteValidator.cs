using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IAsyncDeleteValidator<TEntity> : IAsyncDeleteValidator<TEntity, object>
    {
    }

    public interface IAsyncDeleteValidator<TEntity, TInput>
    {
        Task ValidateForDelete(int id, TInput input = default(TInput));
    }
}
