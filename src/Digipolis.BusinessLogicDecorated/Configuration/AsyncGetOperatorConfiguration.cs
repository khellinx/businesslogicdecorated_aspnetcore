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
        public AsyncGetOperatorConfiguration(Func<IServiceProvider, IAsyncGetOperator<TEntity>> operatorFactory) : base(typeof(TEntity), operatorFactory)
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

        public IAsyncGetOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IGetPostprocessor<TEntity>> PostprocessorFactory = null)
        {
            if (PostprocessorFactory == null)
            {
                PostprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IGetPostprocessor<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPostprocessingDecorator<TEntity>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IGetPostprocessor<TEntity>
        {
            Func<IServiceProvider, IGetPostprocessor<TEntity>> PostprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPostprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPostprocessingDecorator<TEntity>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IGetPreprocessor<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity>
        {
            Func<IServiceProvider, IGetPreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }
    }

    public class AsyncGetOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncGetOperator<TEntity, TInput>>, IAsyncGetOperatorConfiguration<TEntity, TInput>
        where TInput : GetInput<TEntity>
    {
        public AsyncGetOperatorConfiguration(Func<IServiceProvider, IAsyncGetOperator<TEntity, TInput>> operatorFactory) : base(typeof(TEntity), operatorFactory)
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

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IGetPostprocessor<TEntity, TInput>> PostprocessorFactory = null)
        {
            if (PostprocessorFactory == null)
            {
                PostprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IGetPostprocessor<TEntity, TInput>>();
            }

            SurroundWithDecorator((op, serviceProvider) => new AsyncGetPostprocessingDecorator<TEntity, TInput>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IGetPostprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IGetPostprocessor<TEntity, TInput>> PostprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPostprocessor>(serviceProvider);
            };

            SurroundWithDecorator((op, serviceProvider) => new AsyncGetPostprocessingDecorator<TEntity, TInput>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IGetPreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IGetPreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }
    }
}
