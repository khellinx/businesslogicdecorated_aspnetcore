using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration.Extensions
{
    public static class GetOperatorConfigurationExtensions
    {
        public static IOperatorConfiguration<IAsyncGetOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncGetOperator<TEntity>> configuration, Func<IServiceProvider, IGetPreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IGetPreprocessor<TEntity>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncGetOperator<TEntity>> WithPreprocessing<TEntity, TPreprocessor>(this IOperatorConfiguration<IAsyncGetOperator<TEntity>> configuration)
            where TPreprocessor : class, IGetPreprocessor<TEntity>
        {
            Func<IServiceProvider, IGetPreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput>(this IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>> configuration, Func<IServiceProvider, IGetPreprocessor<TEntity, TInput>> preprocessorFactory = null)
            where TInput : IHasIncludes<TEntity>
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IGetPreprocessor<TEntity, TInput>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput, TPreprocessor>(this IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>> configuration)
            where TInput : IHasIncludes<TEntity>
            where TPreprocessor : class, IGetPreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IGetPreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }
    }
}
