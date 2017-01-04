using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Digipolis.BusinessLogicDecorated.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IAsyncDeleteOperatorConfiguration<TEntity> : IOperatorConfiguration<IAsyncDeleteOperator<TEntity>>
    {
        IAsyncDeleteOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncDeleteOperator<TEntity>> operatorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncDeleteOperator<TEntity>;

        IAsyncDeleteOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IDeletePostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IDeletePostprocessor<TEntity>;

        IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncDeletePostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncDeletePostprocessor<TEntity>;

        IAsyncDeleteOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity>;

        IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncDeletePreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncDeletePreprocessor<TEntity>;

        IAsyncDeleteOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity>> validatorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IDeleteValidator<TEntity>;

        IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncValidation(Func<IServiceProvider, IAsyncDeleteValidator<TEntity>> validatorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncDeleteValidator<TEntity>;
    }

    public interface IAsyncDeleteOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>>
    {
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TInput>> preprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncDeleteOperator<TEntity, TInput>;

        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IDeletePostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IDeletePostprocessor<TEntity, TInput>;

        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncDeletePostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncDeletePostprocessor<TEntity, TInput>;

        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity, TInput>;

        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncDeletePreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncDeletePreprocessor<TEntity, TInput>;

        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IDeleteValidator<TEntity, TInput>;

        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncValidation(Func<IServiceProvider, IAsyncDeleteValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncDeleteValidator<TEntity, TInput>;
    }
}
