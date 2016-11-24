﻿using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IQueryPreprocessor<TEntity> : IQueryPreprocessor<TEntity, QueryInput<TEntity>>
    {
    }

    public interface IQueryPreprocessor<TEntity, TInput>
        where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
    {
        void PreprocessForQuery(ref TInput input);
    }
}
