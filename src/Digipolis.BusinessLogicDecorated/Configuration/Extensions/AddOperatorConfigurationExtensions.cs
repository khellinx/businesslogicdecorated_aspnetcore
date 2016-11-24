using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
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
    public static class AddOperatorConfigurationExtensions
    {
        public static IOperatorConfiguration<IAsyncAddOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncAddOperator<TEntity>> configuration, Func<IServiceProvider, IAddPreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddPreprocessor<TEntity>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity>> WithPreprocessing<TEntity, TPreprocessor>(this IOperatorConfiguration<IAsyncAddOperator<TEntity>> configuration)
            where TPreprocessor : class, IAddPreprocessor<TEntity>
        {
            Func<IServiceProvider, IAddPreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput>(this IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> configuration, Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddPreprocessor<TEntity, TInput>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput, TPreprocessor>(this IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> configuration)
            where TPreprocessor : class, IAddPreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity>> WithValidation<TEntity>(this IOperatorConfiguration<IAsyncAddOperator<TEntity>> configuration, Func<IServiceProvider, IAddValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddValidator<TEntity>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity>> WithValidation<TEntity, TValidator>(this IOperatorConfiguration<IAsyncAddOperator<TEntity>> configuration)
            where TValidator : class, IAddValidator<TEntity>
        {
            Func<IServiceProvider, IAddValidator<TEntity>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> WithValidation<TEntity, TInput>(this IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> configuration, Func<IServiceProvider, IAddValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddValidator<TEntity, TInput>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> WithValidation<TEntity, TInput, TValidator>(this IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> configuration)
            where TValidator : class, IAddValidator<TEntity, TInput>
        {
            Func<IServiceProvider, IAddValidator<TEntity, TInput>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return configuration;
        }
    }
}
