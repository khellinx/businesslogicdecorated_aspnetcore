using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IUpdateValidator<TEntity, TInput>
    {
        void Validate(TEntity entity, TInput input = default(TInput));
    }
}
