using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public interface IAsyncDeleteOperator<TEntity> : IAsyncDeleteOperator<TEntity, object>
    {
    }

    public interface IAsyncDeleteOperator<TEntity, TInput> : IAsyncDeleteOperator<TEntity, int, TInput>
    {
    }

    public interface IAsyncDeleteOperator<TEntity, TId, TInput>
    {
        Task DeleteAsync(TId id, TInput input = default(TInput));
    }
}
