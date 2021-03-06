﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Postprocessors
{
    public interface IAsyncDeletePostprocessor<TEntity> : IAsyncDeletePostprocessor<TEntity, object>
    {
    }

    public interface IAsyncDeletePostprocessor<TEntity, TInput> : IAsyncDeletePostprocessor<TEntity, int, TInput>
    {
    }

    public interface IAsyncDeletePostprocessor<TEntity, TId, TInput>
    {
        Task PostprocessForDelete(TId id, TInput input);
    }
}
