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
}
