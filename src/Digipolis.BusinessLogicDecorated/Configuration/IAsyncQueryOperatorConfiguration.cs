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
        IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity>;
    }

    public interface IAsyncQueryOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>>
        where TInput : QueryInput<TEntity>
    {
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity, TInput>;
    }
}