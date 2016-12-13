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
        public AsyncQueryOperatorConfiguration(Func<IServiceProvider, IAsyncQueryOperator<TEntity>> operatorFactory) : base(typeof(TEntity), operatorFactory)
        {
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncQueryOperator<TEntity>> operatorFactory = null)
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

        public IAsyncQueryOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IQueryPostprocessor<TEntity>> PostprocessorFactory = null)
        {
            if (PostprocessorFactory == null)
            {
                PostprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IQueryPostprocessor<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPostprocessingDecorator<TEntity>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IQueryPostprocessor<TEntity>
        {
            Func<IServiceProvider, IQueryPostprocessor<TEntity>> PostprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPostprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPostprocessingDecorator<TEntity>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IQueryPreprocessor<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity>
        {
            Func<IServiceProvider, IQueryPreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }
    }

    public class AsyncQueryOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>>, IAsyncQueryOperatorConfiguration<TEntity, TInput>
        where TInput : QueryInput<TEntity>
    {
        public AsyncQueryOperatorConfiguration(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> operatorFactory) : base(typeof(TEntity), operatorFactory)
        {
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> operatorFactory = null)
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

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IQueryPostprocessor<TEntity, TInput>> PostprocessorFactory = null)
        {
            if (PostprocessorFactory == null)
            {
                PostprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IQueryPostprocessor<TEntity, TInput>>();
            }

            SurroundWithDecorator((op, serviceProvider) => new AsyncQueryPostprocessingDecorator<TEntity, TInput>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IQueryPostprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IQueryPostprocessor<TEntity, TInput>> PostprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPostprocessor>(serviceProvider);
            };

            SurroundWithDecorator((op, serviceProvider) => new AsyncQueryPostprocessingDecorator<TEntity, TInput>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IQueryPreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }
    }
}
