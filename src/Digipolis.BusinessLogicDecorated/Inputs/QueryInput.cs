﻿using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Inputs
{
    public class QueryInput<TEntity> : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
    {
        public Func<IQueryable<TEntity>, IQueryable<TEntity>> Filter { get; set; }
        public Func<IQueryable<TEntity>, IQueryable<TEntity>> Includes { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> Order { get; set; }
    }
}