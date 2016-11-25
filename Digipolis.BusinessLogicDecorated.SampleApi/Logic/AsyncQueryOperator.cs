using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Paging;
using Digipolis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic
{
    public class AsyncQueryOperator<TEntity> : AsyncQueryOperator<TEntity, QueryInput<TEntity>>, IAsyncQueryOperator<TEntity>
    {
        public AsyncQueryOperator(IUowProvider uowProvider) : base(uowProvider)
        {
        }
    }

    public class AsyncQueryOperator<TEntity, TInput> : IAsyncQueryOperator<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        private IUowProvider _uowProvider;

        public AsyncQueryOperator(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(TInput input = default(TInput))
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<TEntity>();
                return await repository.QueryAsync(input?.Filter, input?.Order, input?.Includes);
            }
        }

        public virtual async Task<PagedCollection<TEntity>> QueryAsync(Page page, TInput input = default(TInput))
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
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
}
