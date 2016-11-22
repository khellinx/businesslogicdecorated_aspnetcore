using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IDeleteValidator<TEntity, TInput>
    {
        void Validate(int id, TInput input = default(TInput));
    }
}
