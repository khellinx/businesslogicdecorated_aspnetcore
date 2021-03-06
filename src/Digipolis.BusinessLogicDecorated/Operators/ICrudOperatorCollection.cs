﻿using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Operators
{
    public interface ICrudOperatorCollection<TEntity> : ICrudOperatorCollection<TEntity, GetInput<TEntity>, QueryInput<TEntity>>
    {
    }

    public interface ICrudOperatorCollection<TEntity, TGetInput, TQueryInput>
        where TGetInput : GetInput<TEntity>
        where TQueryInput : QueryInput<TEntity>
    {
        Task<TEntity> GetAsync(int id, TGetInput input = default(TGetInput));
        Task<IEnumerable<TEntity>> QueryAsync(TQueryInput input = default(TQueryInput));
        Task<PagedCollection<TEntity>> QueryAsync(Page page, TQueryInput input = default(TQueryInput));
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}
