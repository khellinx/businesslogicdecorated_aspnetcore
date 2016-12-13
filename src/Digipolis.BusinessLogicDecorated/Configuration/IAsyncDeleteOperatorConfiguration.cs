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

        IAsyncDeleteOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IDeletePostprocessor<TEntity>> PostprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IDeletePostprocessor<TEntity>;

        IAsyncDeleteOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity>;

        IAsyncDeleteOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity>> validatorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IDeleteValidator<TEntity>;
    }

    public interface IAsyncDeleteOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>>
    {
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TInput>> preprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncDeleteOperator<TEntity, TInput>;

        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IDeletePostprocessor<TEntity, TInput>> PostprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IDeletePostprocessor<TEntity, TInput>;

        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity, TInput>;

        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IDeleteValidator<TEntity, TInput>;
    }
}
