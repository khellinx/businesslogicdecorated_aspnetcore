﻿using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Digipolis.BusinessLogicDecorated.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IAsyncAddOperatorConfiguration<TEntity> : IOperatorConfiguration<IAsyncAddOperator<TEntity>>
    {
        IAsyncAddOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncAddOperator<TEntity>> operatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncAddOperator<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IAddPostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAddPostprocessor<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncAddPostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncAddPostprocessor<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncAddPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncAddPreprocessor<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IAddValidator<TEntity>> validatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithAsyncValidation(Func<IServiceProvider, IAsyncAddValidator<TEntity>> validatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncAddValidator<TEntity>;
    }

    public interface IAsyncAddOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>>
    {
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncAddOperator<TEntity, TInput>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncAddOperator<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IAddPostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAddPostprocessor<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncAddPostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncAddPostprocessor<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncAddPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncAddPreprocessor<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IAddValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncValidation(Func<IServiceProvider, IAsyncAddValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncAddValidator<TEntity, TInput>;
    }
}
