using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.DataAccess;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic
{
    public class AsyncGetOperator<TEntity> : AsyncGetOperator<TEntity, GetInput<TEntity>>, IAsyncGetOperator<TEntity>
    {
        public AsyncGetOperator(IUowProvider uowProvider) : base(uowProvider)
        {
        }
    }

    public class AsyncGetOperator<TEntity, TInput> : IAsyncGetOperator<TEntity, TInput>
        where TInput : IHasIncludes<TEntity>
    {
        private IUowProvider _uowProvider;

        public AsyncGetOperator(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public virtual async Task<TEntity> GetAsync(int id, TInput input = default(TInput))
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<TEntity>();
                return await repository.GetAsync(id, input?.Includes);
            }
        }
    }
}
