using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic
{
    public class AsyncDeleteOperator<TEntity> : AsyncDeleteOperator<TEntity, object>, IAsyncDeleteOperator<TEntity>
    {
        public AsyncDeleteOperator(IUowProvider uowProvider) : base(uowProvider)
        {
        }
    }

    public class AsyncDeleteOperator<TEntity, TInput> : IAsyncDeleteOperator<TEntity, TInput>
    {
        private IUowProvider _uowProvider;

        public AsyncDeleteOperator(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public virtual async Task DeleteAsync(int id, TInput input = default(TInput))
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<TEntity>();
                repository.Remove(id);

                await uow.SaveChangesAsync();
            }
        }
    }
}
