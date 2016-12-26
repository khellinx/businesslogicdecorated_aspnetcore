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
    public interface IAsyncUpdateOperatorConfiguration<TEntity> : IOperatorConfiguration<IAsyncUpdateOperator<TEntity>>
    {
        IAsyncUpdateOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncUpdateOperator<TEntity>> operatorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncUpdateOperator<TEntity>;

        IAsyncUpdateOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IUpdatePostprocessor<TEntity>> PostprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IUpdatePostprocessor<TEntity>;

        IAsyncUpdateOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IUpdatePreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IUpdatePreprocessor<TEntity>;

        IAsyncUpdateOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IUpdateValidator<TEntity>> validatorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IUpdateValidator<TEntity>;

        IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncValidation(Func<IServiceProvider, IAsyncUpdateValidator<TEntity>> validatorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncUpdateValidator<TEntity>;
    }

    public interface IAsyncUpdateOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>>
    {
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, TInput>> preprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncUpdateOperator<TEntity, TInput>;

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IUpdatePostprocessor<TEntity, TInput>> PostprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IUpdatePostprocessor<TEntity, TInput>;

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IUpdatePreprocessor<TEntity, TInput>;

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IUpdateValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IUpdateValidator<TEntity, TInput>;

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncValidation(Func<IServiceProvider, IAsyncUpdateValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncUpdateValidator<TEntity, TInput>;
    }
}
