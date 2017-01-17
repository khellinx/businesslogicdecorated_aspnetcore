using Digipolis.BusinessLogicDecorated.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public interface IAsyncGetOperator<TEntity> : IAsyncGetOperator<TEntity, GetInput<TEntity>>
    {
    }

    public interface IAsyncGetOperator<TEntity, TInput> : IAsyncGetOperator<TEntity, int, TInput>
        where TInput : GetInput<TEntity>
    {
    }

    public interface IAsyncGetOperator<TEntity, TId, TInput>
        where TInput : GetInput<TEntity>
    {
        Task<TEntity> GetAsync(TId id, TInput input = default(TInput));
    }
}
