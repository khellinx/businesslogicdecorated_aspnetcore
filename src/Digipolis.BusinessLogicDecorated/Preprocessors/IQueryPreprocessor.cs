﻿using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Paging;
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
        where TInput : QueryInput<TEntity>
    {
        void PreprocessForQuery(ref TInput input);
        void PreprocessForQuery(ref Page page, ref TInput input);
    }
}
