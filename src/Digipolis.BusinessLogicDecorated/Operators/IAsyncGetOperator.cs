using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public interface IAsyncGetOperator<TEntity> : IAsyncGetOperator<TEntity, GetInput<TEntity>>
    {
    }

    public interface IAsyncGetOperator<TEntity, TInput>
        where TInput : class, IHasIncludes<TEntity>
    {
        Task<TEntity> GetAsync(int id, TInput input = null);
    }
}
