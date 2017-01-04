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

        IAsyncUpdateOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IUpdatePostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IUpdatePostprocessor<TEntity>;

        IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncUpdatePostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncUpdatePostprocessor<TEntity>;

        IAsyncUpdateOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IUpdatePreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IUpdatePreprocessor<TEntity>;

        IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncUpdatePreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncUpdatePreprocessor<TEntity>;

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

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IUpdatePostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IUpdatePostprocessor<TEntity, TInput>;

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncUpdatePostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncUpdatePostprocessor<TEntity, TInput>;

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IUpdatePreprocessor<TEntity, TInput>;

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncUpdatePreprocessor<TEntity, TInput>;

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IUpdateValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IUpdateValidator<TEntity, TInput>;

        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncValidation(Func<IServiceProvider, IAsyncUpdateValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncUpdateValidator<TEntity, TInput>;
    }
}
