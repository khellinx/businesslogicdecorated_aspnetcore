using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IAsyncQueryOperatorConfiguration<TEntity> : IOperatorConfiguration<IAsyncQueryOperator<TEntity>>
    {
        IAsyncQueryOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncQueryOperator<TEntity>> operatorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncQueryOperator<TEntity>;

        IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity>;
    }

    public interface IAsyncQueryOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>>
        where TInput : QueryInput<TEntity>
    {
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncQueryOperator<TEntity, TInput>;

        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity, TInput>;
    }
}