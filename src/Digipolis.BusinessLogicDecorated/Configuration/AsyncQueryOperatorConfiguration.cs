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
    public class AsyncQueryOperatorConfiguration<TEntity> : OperatorConfiguration<IAsyncQueryOperator<TEntity>>, IAsyncQueryOperatorConfiguration<TEntity>
    {
        public AsyncQueryOperatorConfiguration(Func<IServiceProvider, IAsyncQueryOperator<TEntity>> operatorFactory) : base(operatorFactory)
        {
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncQueryOperator<TEntity>> operatorFactory)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncQueryOperator<TEntity>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IQueryPostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncQueryPostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IQueryPostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncQueryPostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncQueryPostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncQueryPostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncQueryPostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncQueryPostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncQueryPreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncQueryPreprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncQueryPreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncQueryPreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncQueryPreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncQueryPreprocessingDecorator<TEntity>(op, dep));

            return this;
        }
    }

    public class AsyncQueryOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>>, IAsyncQueryOperatorConfiguration<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        public AsyncQueryOperatorConfiguration(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> operatorFactory)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncQueryOperator<TEntity, TInput>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IQueryPostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncQueryPostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IQueryPostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncQueryPostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncQueryPostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncQueryPostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncQueryPostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncQueryPostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncQueryPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncQueryPreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }
    }
}
