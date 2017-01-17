using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public class AsyncGetOperatorConfiguration<TEntity> : OperatorConfiguration<IAsyncGetOperator<TEntity>>, IAsyncGetOperatorConfiguration<TEntity>
    {
        public AsyncGetOperatorConfiguration(Func<IServiceProvider, IAsyncGetOperator<TEntity>> operatorFactory) : base(operatorFactory)
        {
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncGetOperator<TEntity>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncGetOperator<TEntity>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IGetPostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncGetPostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IGetPostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncGetPostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncGetPostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncGetPostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncGetPostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncGetPostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncGetPreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncGetPreprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncGetPreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncGetPreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncGetPreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncGetPreprocessingDecorator<TEntity>(op, dep));

            return this;
        }
    }

    public class AsyncGetOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncGetOperator<TEntity, TInput>>, IAsyncGetOperatorConfiguration<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetOperatorConfiguration(Func<IServiceProvider, IAsyncGetOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncGetOperator<TEntity, TInput>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncGetOperator<TEntity, TInput>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IGetPostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncGetPostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IGetPostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncGetPostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncGetPostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncGetPostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncGetPostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncGetPostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncGetPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncGetPreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }
    }

    public class AsyncGetOperatorConfiguration<TEntity, TId, TInput> : OperatorConfiguration<IAsyncGetOperator<TEntity, TId, TInput>>, IAsyncGetOperatorConfiguration<TEntity, TId, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetOperatorConfiguration(Func<IServiceProvider, IAsyncGetOperator<TEntity, TId, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncGetOperator<TEntity, TId, TInput>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncGetOperator<TEntity, TId, TInput>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithPostprocessing(Func<IServiceProvider, IGetPostprocessor<TEntity, TId, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncGetPostprocessingDecorator<TEntity, TId, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IGetPostprocessor<TEntity, TId, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncGetPostprocessingDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncGetPostprocessor<TEntity, TId, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncGetPostprocessingDecorator<TEntity, TId, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncGetPostprocessor<TEntity, TId, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncGetPostprocessingDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity, TId, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncGetPreprocessingDecorator<TEntity, TId, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity, TId, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncGetPreprocessingDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncGetPreprocessor<TEntity, TId, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncGetPreprocessingDecorator<TEntity, TId, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TId, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncGetPreprocessor<TEntity, TId, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncGetPreprocessingDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }
    }
}
