using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic
{
    public class AsyncUpdateOperator<TEntity> : AsyncUpdateOperator<TEntity, object>, IAsyncUpdateOperator<TEntity>
    {
        public AsyncUpdateOperator(IUowProvider uowProvider) : base(uowProvider)
        {
        }
    }

    public class AsyncUpdateOperator<TEntity, TInput> : IAsyncUpdateOperator<TEntity, TInput>
    {
        private IUowProvider _uowProvider;

        public AsyncUpdateOperator(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, TInput input = default(TInput))
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<TEntity>();
                repository.Update(entity);

                await uow.SaveChangesAsync();

                return entity;
            }
        }
    }
}
