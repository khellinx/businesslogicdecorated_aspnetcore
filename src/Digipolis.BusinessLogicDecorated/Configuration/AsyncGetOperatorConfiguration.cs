using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
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
