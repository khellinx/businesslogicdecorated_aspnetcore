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
    public interface IAsyncAddOperatorConfiguration<TEntity> : IOperatorConfiguration<IAsyncAddOperator<TEntity>>
    {
        IAsyncAddOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncAddOperator<TEntity>> operatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncAddOperator<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IAddPostprocessor<TEntity>> PostprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAddPostprocessor<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IAddValidator<TEntity>> validatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity>;
    }

    public interface IAsyncAddOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>>
    {
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncAddOperator<TEntity, TInput>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncAddOperator<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IAddPostprocessor<TEntity, TInput>> PostprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAddPostprocessor<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IAddValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity, TInput>;
    }
}
