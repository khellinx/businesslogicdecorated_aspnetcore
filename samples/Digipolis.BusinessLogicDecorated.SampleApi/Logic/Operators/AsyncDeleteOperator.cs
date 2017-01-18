using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;
using Digipolis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Operators
{
    public class AsyncDeleteOperator<TEntity> : AsyncDeleteOperator<TEntity, object>, IAsyncDeleteOperator<TEntity>
    {
        public AsyncDeleteOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }
    }

    public class AsyncDeleteOperator<TEntity, TInput> : Worker, IAsyncDeleteOperator<TEntity, TInput>
    {
        public AsyncDeleteOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }

        public async Task DeleteAsync(int id, TInput input = default(TInput))
        {
            var uow = UnitOfWorkScope.GetUnitOfWork(true);

            var repository = uow.GetRepository<TEntity>();

            repository.Remove(id);
            await uow.SaveChangesAsync();
        }
    }
}
