using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IDeleteValidator<TEntity> : IDeleteValidator<TEntity, object>
    {
    }

    public interface IDeleteValidator<TEntity, TInput>
    {
        void ValidateForDelete(int id, TInput input = default(TInput));
    }
}
