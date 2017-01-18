using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Paging;
using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;
using Digipolis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Operators
{
    public class AsyncQueryOperator<TEntity> : AsyncQueryOperator<TEntity, QueryInput<TEntity>>, IAsyncQueryOperator<TEntity>
    {
        public AsyncQueryOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }
    }

    public class AsyncQueryOperator<TEntity, TInput> : Worker, IAsyncQueryOperator<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        public AsyncQueryOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(TInput input = null)
        {
            var uow = UnitOfWorkScope.GetUnitOfWork(true);

            var repository = uow.GetRepository<TEntity>();
            return await repository.QueryAsync(input?.Filter, input?.Order, input?.Includes);
        }

        public async Task<PagedCollection<TEntity>> QueryAsync(Page page, TInput input = null)
        {
            var uow = UnitOfWorkScope.GetUnitOfWork(true);

            var repository = uow.GetRepository<TEntity>();
            var startRow = (page.Number - 1) * page.Size;

            var result = new PagedCollection<TEntity>()
            {
                Page = page,
                Data = await repository.QueryPageAsync(startRow, page.Size, input?.Filter, input?.Order, input?.Includes),
                TotalCount = await repository.CountAsync(input?.Filter)
            };

            return result;
        }
    }
}
