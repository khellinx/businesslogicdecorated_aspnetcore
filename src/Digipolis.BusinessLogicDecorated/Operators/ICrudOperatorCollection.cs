using Digipolis.BusinessLogicDecorated.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public interface ICrudOperatorCollection<TEntity, TGetInput, TQueryInput>
        where TGetInput : GetInput<TEntity>
        where TQueryInput : QueryInput<TEntity>
    {
        IAsyncGetOperator<TEntity, TGetInput> AsyncGetOperator { get; }
        IAsyncQueryOperator<TEntity, TQueryInput> AsyncQueryOperator { get; }
        IAsyncAddOperator<TEntity> AsyncAddOperator { get; }
        IAsyncUpdateOperator<TEntity> AsyncUpdateOperator { get; }
        IAsyncDeleteOperator<TEntity> AsyncDeleteOperator { get; }

        Task<TEntity> GetAsync(int id, TGetInput input = default(TGetInput));
        Task<IEnumerable<TEntity>> QueryAsync(TQueryInput input = default(TQueryInput));
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}
