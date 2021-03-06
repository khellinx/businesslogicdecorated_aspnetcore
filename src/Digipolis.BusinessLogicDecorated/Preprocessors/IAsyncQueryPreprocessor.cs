﻿using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Preprocessors
{
    public interface IAsyncQueryPreprocessor<TEntity> : IAsyncQueryPreprocessor<TEntity, QueryInput<TEntity>>
    {
    }

    public interface IAsyncQueryPreprocessor<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        Task PreprocessForQuery(TInput input);
        Task PreprocessForQuery(Page page, TInput input);
    }
}
