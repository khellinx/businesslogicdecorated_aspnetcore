using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public class AsyncAddOperator<TEntity, TInput> : IAsyncAddOperator<TEntity, TInput>
        where TInput : class
    {
        public Task<TEntity> AddAsync(TEntity entity, TInput input = default(TInput))
        {
            throw new NotImplementedException();
        }
    }
}
