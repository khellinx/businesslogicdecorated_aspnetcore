using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
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

        IAsyncQueryOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IQueryPostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IQueryPostprocessor<TEntity>;

        IAsyncQueryOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncQueryPostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncQueryPostprocessor<TEntity>;

        IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity>;

        IAsyncQueryOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncQueryPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncQueryPreprocessor<TEntity>;
    }

    public interface IAsyncQueryOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>>
        where TInput : QueryInput<TEntity>
    {
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncQueryOperator<TEntity, TInput>;

        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IQueryPostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IQueryPostprocessor<TEntity, TInput>;

        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncQueryPostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncQueryPostprocessor<TEntity, TInput>;

        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity, TInput>;

        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncQueryPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncQueryOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncQueryPreprocessor<TEntity, TInput>;
    }
}