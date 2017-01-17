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
    public interface IAsyncGetOperatorConfiguration<TEntity> : IOperatorConfiguration<IAsyncGetOperator<TEntity>>
    {
        IAsyncGetOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncGetOperator<TEntity>> operatorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncGetOperator<TEntity>;

        IAsyncGetOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IGetPostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IGetPostprocessor<TEntity>;

        IAsyncGetOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncGetPostprocessor<TEntity>> postprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncGetPostprocessor<TEntity>;

        IAsyncGetOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity>;

        IAsyncGetOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncGetPreprocessor<TEntity>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncGetPreprocessor<TEntity>;
    }

    public interface IAsyncGetOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>>
        where TInput : GetInput<TEntity>
    {
        IAsyncGetOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncGetOperator<TEntity, TInput>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncGetOperator<TEntity, TInput>;

        IAsyncGetOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IGetPostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IGetPostprocessor<TEntity, TInput>;

        IAsyncGetOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncGetPostprocessor<TEntity, TInput>> postprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncGetPostprocessor<TEntity, TInput>;

        IAsyncGetOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity, TInput>;

        IAsyncGetOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncGetPreprocessor<TEntity, TInput>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncGetPreprocessor<TEntity, TInput>;
    }

    public interface IAsyncGetOperatorConfiguration<TEntity, TId, TInput> : IOperatorConfiguration<IAsyncGetOperator<TEntity, TId, TInput>>
        where TInput : GetInput<TEntity>
    {
        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncGetOperator<TEntity, TId, TInput>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncGetOperator<TEntity, TId, TInput>;

        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithPostprocessing(Func<IServiceProvider, IGetPostprocessor<TEntity, TId, TInput>> postprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IGetPostprocessor<TEntity, TId, TInput>;

        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncGetPostprocessor<TEntity, TId, TInput>> postprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncGetPostprocessor<TEntity, TId, TInput>;

        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity, TId, TInput>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity, TId, TInput>;

        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncGetPreprocessor<TEntity, TId, TInput>> preprocessorFactory = null);
        IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncGetPreprocessor<TEntity, TId, TInput>;
    }
}
