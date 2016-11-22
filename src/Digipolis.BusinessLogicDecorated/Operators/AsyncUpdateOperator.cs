using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public class AsyncUpdateOperator<TEntity, TInput> : IAsyncUpdateOperator<TEntity, TInput>
    {
        public Task<TEntity> UpdateAsync(TEntity entity, TInput input = default(TInput))
        {
            throw new NotImplementedException();
        }
    }
}
