using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IAsyncQueryOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>>
        where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
    {
    }
}