using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public interface IAsyncAddOperator<TEntity> : IAsyncAddOperator<TEntity, object>
    {
    }

    public interface IAsyncAddOperator<TEntity, TInput>
        where TInput : class
    {
        Task<TEntity> AddAsync(TEntity entity, TInput input = null);
    }
}
