using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;
using Digipolis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic
{
    public class AsyncAddOperator<TEntity> : AsyncAddOperator<TEntity, object>, IAsyncAddOperator<TEntity>
    {
        public AsyncAddOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }
    }

    public class AsyncAddOperator<TEntity, TInput> : Worker, IAsyncAddOperator<TEntity, TInput>
    {
        public AsyncAddOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }

        public async Task<TEntity> AddAsync(TEntity entity, TInput input = default(TInput))
        {
            var uow = UnitOfWorkScope.GetUnitOfWork(true);

            var repository = uow.GetRepository<TEntity>();

            repository.Add(entity);
            await uow.SaveChangesAsync();

            return entity;
        }
    }
}
