﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Validators
{
    public interface IAddValidator<TEntity> : IAddValidator<TEntity, object>
    {
    }

    public interface IAddValidator<TEntity, TInput>
    {
        void ValidateForAdd(TEntity entity, TInput input = default(TInput));
    }
}
