using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.DataAccess;
using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Operators
{
    public class AsyncGetOperator<TEntity> : AsyncGetOperator<TEntity, GetInput<TEntity>>, IAsyncGetOperator<TEntity>
    {
        public AsyncGetOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }
    }

    public class AsyncGetOperator<TEntity, TInput> : Worker, IAsyncGetOperator<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }

        public async Task<TEntity> GetAsync(int id, TInput input = null)
        {
            var uow = UnitOfWorkScope.GetUnitOfWork(true);

            var repository = uow.GetRepository<TEntity>();
            var result = await repository.GetAsync(id, input?.Includes);

            if (result == null)
            {
                throw new Exception("Entity could not be found.");
            }

            return result;
        }
    }
}
