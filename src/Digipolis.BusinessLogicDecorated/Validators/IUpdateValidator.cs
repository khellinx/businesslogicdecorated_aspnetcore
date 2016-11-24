using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IUpdateValidator<TEntity> : IUpdateValidator<TEntity, object>
    {
    }

    public interface IUpdateValidator<TEntity, TInput>
    {
        void ValidateForUpdate(TEntity entity, TInput input = default(TInput));
    }
}
