using Digipolis.BusinessLogicDecorated.Operators;
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
        IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity>;

        IAsyncAddOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IAddValidator<TEntity>> validatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity>;
    }

    public interface IAsyncAddOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>>
    {
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity, TInput>;

        IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IAddValidator<TEntity, TInput>> validatorFactory = null);
        IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity, TInput>;
    }
}
