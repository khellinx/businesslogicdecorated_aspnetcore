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
    public static class QueryOperatorConfigurationExtensions
    {
        public static IOperatorConfiguration<IAsyncQueryOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncQueryOperator<TEntity>> configuration, Func<IServiceProvider, IQueryPreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IQueryPreprocessor<TEntity>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncQueryOperator<TEntity>> WithPreprocessing<TEntity, TPreprocessor>(this IOperatorConfiguration<IAsyncQueryOperator<TEntity>> configuration)
            where TPreprocessor : class, IQueryPreprocessor<TEntity>
        {
            Func<IServiceProvider, IQueryPreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput>(this IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>> configuration, Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = null)
            where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IQueryPreprocessor<TEntity, TInput>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput, TPreprocessor>(this IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>> configuration)
            where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
            where TPreprocessor : class, IQueryPreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }
    }
}
