using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public class AsyncDeleteOperator<TEntity, TInput> : IAsyncDeleteOperator<TEntity, TInput>
    {
        public Task<TEntity> DeleteAsync(int id, TInput input = null)
        {
            throw new NotImplementedException();
        }
    }
}
