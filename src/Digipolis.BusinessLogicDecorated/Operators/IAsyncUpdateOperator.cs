using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public interface IAsyncUpdateOperator<TEntity> : IAsyncUpdateOperator<TEntity, object>
    {
    }

    public interface IAsyncUpdateOperator<TEntity, TInput>
        where TInput : class
    {
        Task<TEntity> UpdateAsync(TEntity entity, TInput input = null);
    }
}
