using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
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
        where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
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
    }
}
