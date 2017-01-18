using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;
using Digipolis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Operators
{
    public class AsyncUpdateOperator<TEntity> : AsyncUpdateOperator<TEntity, object>, IAsyncUpdateOperator<TEntity>
    {
        public AsyncUpdateOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }
    }

    public class AsyncUpdateOperator<TEntity, TInput> : Worker, IAsyncUpdateOperator<TEntity, TInput>
    {
        public AsyncUpdateOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, TInput input = default(TInput))
        {
            var uow = UnitOfWorkScope.GetUnitOfWork(true);

            var repository = uow.GetRepository<TEntity>();

            repository.Update(entity);
            await uow.SaveChangesAsync();

            return entity;
        }
    }
}
