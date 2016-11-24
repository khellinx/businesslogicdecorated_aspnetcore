using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic
{
    public class AsyncAddOperator<TEntity> : AsyncAddOperator<TEntity, object>, IAsyncAddOperator<TEntity>
    {
        public AsyncAddOperator(IUowProvider uowProvider) : base(uowProvider)
        {
        }
    }

    public class AsyncAddOperator<TEntity, TInput> : IAsyncAddOperator<TEntity, TInput>
    {
        private IUowProvider _uowProvider;

        public AsyncAddOperator(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, TInput input = default(TInput))
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<TEntity>();
                repository.Add(entity);

                await uow.SaveChangesAsync();

                return entity;
            }
        }
    }
}
