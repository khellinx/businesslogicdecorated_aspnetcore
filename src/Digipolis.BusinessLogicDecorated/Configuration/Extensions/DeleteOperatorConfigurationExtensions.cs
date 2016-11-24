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
    public static class DeleteOperatorConfigurationExtensions
    {
        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> configuration, Func<IServiceProvider, IDeletePreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeletePreprocessor<TEntity>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> WithPreprocessing<TEntity, TPreprocessor>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> configuration)
            where TPreprocessor : class, IDeletePreprocessor<TEntity>
        {
            Func<IServiceProvider, IDeletePreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> configuration, Func<IServiceProvider, IDeletePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeletePreprocessor<TEntity, TInput>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> WithPreprocessing<TEntity, TInput, TPreprocessor>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> configuration)
            where TPreprocessor : class, IDeletePreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IDeletePreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> WithValidation<TEntity>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> configuration, Func<IServiceProvider, IDeleteValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeleteValidator<TEntity>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeleteValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> WithValidation<TEntity, TValidator>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> configuration)
            where TValidator : class, IDeleteValidator<TEntity>
        {
            Func<IServiceProvider, IDeleteValidator<TEntity>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeleteValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> WithValidation<TEntity, TInput>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> configuration, Func<IServiceProvider, IDeleteValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeleteValidator<TEntity, TInput>>();
            }

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeleteValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> WithValidation<TEntity, TInput, TValidator>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> configuration)
            where TValidator : class, IDeleteValidator<TEntity, TInput>
        {
            Func<IServiceProvider, IDeleteValidator<TEntity, TInput>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            configuration.InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeleteValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return configuration;
        }
    }
}
