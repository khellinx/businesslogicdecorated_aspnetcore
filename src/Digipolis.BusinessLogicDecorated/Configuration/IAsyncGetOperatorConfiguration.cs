using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IAsyncGetOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>>
        where TInput : IHasIncludes<TEntity>
    {
    }
}
