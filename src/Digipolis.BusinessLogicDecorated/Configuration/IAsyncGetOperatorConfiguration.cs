using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IAsyncGetOperatorConfiguration<TEntity> : IOperatorConfiguration<IAsyncGetOperator<TEntity>>
    {
        IAsyncGetOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncGetOperator<TEntity>> operatorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncGetOperator<TEntity>;

        IAsyncGetOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity>;
    }

    public interface IAsyncGetOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>>
        where TInput : GetInput<TEntity>
    {
        IAsyncGetOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncGetOperator<TEntity, TInput>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncGetOperator<TEntity, TInput>;

        IAsyncGetOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity, TInput>;
    }
}
