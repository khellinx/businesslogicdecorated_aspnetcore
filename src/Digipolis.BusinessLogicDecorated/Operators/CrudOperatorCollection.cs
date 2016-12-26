using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public class CrudOperatorCollection<TEntity> : CrudOperatorCollection<TEntity, GetInput<TEntity>, QueryInput<TEntity>>, ICrudOperatorCollection<TEntity>
    {
        public CrudOperatorCollection(IAsyncGetOperator<TEntity, GetInput<TEntity>> asyncGetOperator, IAsyncQueryOperator<TEntity, QueryInput<TEntity>> asyncQueryOperator, IAsyncAddOperator<TEntity> asyncAddOperator, IAsyncUpdateOperator<TEntity> asyncUpdateOperator, IAsyncDeleteOperator<TEntity> asyncDeleteOperator) : base(asyncGetOperator, asyncQueryOperator, asyncAddOperator, asyncUpdateOperator, asyncDeleteOperator)
        {
        }
    }

    public class CrudOperatorCollection<TEntity, TGetInput, TQueryInput> : ICrudOperatorCollection<TEntity, TGetInput, TQueryInput>
        where TGetInput : GetInput<TEntity>
        where TQueryInput : QueryInput<TEntity>
    {
        public CrudOperatorCollection(
            IAsyncGetOperator<TEntity, TGetInput> asyncGetOperator,
            IAsyncQueryOperator<TEntity, TQueryInput> asyncQueryOperator,
            IAsyncAddOperator<TEntity> asyncAddOperator,
            IAsyncUpdateOperator<TEntity> asyncUpdateOperator,
            IAsyncDeleteOperator<TEntity> asyncDeleteOperator
            )
        {
            AsyncGetOperator = asyncGetOperator;
            AsyncQueryOperator = asyncQueryOperator;
            AsyncAddOperator = asyncAddOperator;
            AsyncUpdateOperator = asyncUpdateOperator;
            AsyncDeleteOperator = asyncDeleteOperator;
        }

        public IAsyncGetOperator<TEntity, TGetInput> AsyncGetOperator { get; private set; }
        public IAsyncQueryOperator<TEntity, TQueryInput> AsyncQueryOperator { get; private set; }
        public IAsyncAddOperator<TEntity> AsyncAddOperator { get; private set; }
        public IAsyncUpdateOperator<TEntity> AsyncUpdateOperator { get; private set; }
        public IAsyncDeleteOperator<TEntity> AsyncDeleteOperator { get; private set; }

        public Task<TEntity> GetAsync(int id, TGetInput input = default(TGetInput))
        {
            if (AsyncGetOperator == null)
            {
                throw new InvalidOperationException($"There is no Get operator specified for entity '{typeof(TEntity).Name}'.");
            }

            return AsyncGetOperator.GetAsync(id, input);
        }

        public Task<IEnumerable<TEntity>> QueryAsync(TQueryInput input = default(TQueryInput))
        {
            if (AsyncQueryOperator == null)
            {
                throw new InvalidOperationException($"There is no Query operator specified for entity '{typeof(TEntity).Name}'.");
            }

            return AsyncQueryOperator.QueryAsync(input);
        }

        public Task<PagedCollection<TEntity>> QueryAsync(Page page, TQueryInput input = default(TQueryInput))
        {
            if (AsyncQueryOperator == null)
            {
                throw new InvalidOperationException($"There is no Query operator specified for entity '{typeof(TEntity).Name}'.");
            }

            return AsyncQueryOperator.QueryAsync(page, input);
        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            if (AsyncAddOperator == null)
            {
                throw new InvalidOperationException($"There is no Add operator specified for entity '{typeof(TEntity).Name}'.");
            }

            return AsyncAddOperator.AddAsync(entity);
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (AsyncUpdateOperator == null)
            {
                throw new InvalidOperationException($"There is no Update operator specified for entity '{typeof(TEntity).Name}'.");
            }

            return AsyncUpdateOperator.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            if (AsyncDeleteOperator == null)
            {
                throw new InvalidOperationException($"There is no Delete operator specified for entity '{typeof(TEntity).Name}'.");
            }

            return AsyncDeleteOperator.DeleteAsync(id);
        }
    }
}
