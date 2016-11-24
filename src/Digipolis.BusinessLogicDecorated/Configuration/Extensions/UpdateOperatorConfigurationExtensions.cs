using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Digipolis.BusinessLogicDecorated.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration.Extensions
{
    public static class UpdateOperatorConfigurationExtensions
    {
        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> configuration, Func<IServiceProvider, IUpdatePreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdatePreprocessor<TEntity>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> WithPreprocessing<TEntity, TPreprocessor>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> configuration)
            where TPreprocessor : class, IUpdatePreprocessor<TEntity>
        {
            Func<IServiceProvider, IUpdatePreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> configuration, Func<IServiceProvider, IUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdatePreprocessor<TEntity, TInput>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput, TPreprocessor>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> configuration)
            where TPreprocessor : class, IUpdatePreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> WithValidation<TEntity>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> configuration, Func<IServiceProvider, IUpdateValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdateValidator<TEntity>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> WithValidation<TEntity, TValidator>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> configuration)
            where TValidator : class, IUpdateValidator<TEntity>
        {
            Func<IServiceProvider, IUpdateValidator<TEntity>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> WithValidation<TEntity, TInput>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> configuration, Func<IServiceProvider, IUpdateValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdateValidator<TEntity, TInput>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> WithValidation<TEntity, TInput, TValidator>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> configuration)
            where TValidator : class, IUpdateValidator<TEntity, TInput>
        {
            Func<IServiceProvider, IUpdateValidator<TEntity, TInput>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return configuration;
        }
    }
}
