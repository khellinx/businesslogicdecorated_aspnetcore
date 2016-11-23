using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity Get(int id);
        IEnumerable<TEntity> Query(Func<IQueryable<TEntity>, IQueryable<TEntity>> includes, Func<IQueryable<TEntity>, IQueryable<TEntity>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(int id);
    }
}
