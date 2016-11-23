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

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public static class OperatorConfigurationExtensions
    {
        public static IOperatorConfiguration<TOperator> InsertDecoratorBeforeOperator<TOperator>(this IOperatorConfiguration<TOperator> configuration, Func<TOperator, IServiceProvider, TOperator> decorator)
            where TOperator : class
        {
            configuration.Decorators.Insert(0, decorator);

            return configuration;
        }

        public static IOperatorConfiguration<TOperator> SurroundWithDecorator<TOperator>(this IOperatorConfiguration<TOperator> configuration, Func<TOperator, IServiceProvider, TOperator> decorator)
            where TOperator : class
        {
            configuration.Decorators.Add(decorator);

            return configuration;
        }

        #region GetOperator

        public static IOperatorConfiguration<IAsyncGetOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncGetOperator<TEntity>> configuration, Func<IServiceProvider, IGetPreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IGetPreprocessor<TEntity>>();
            }

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

        #endregion

        #region QueryOperator

        public static IOperatorConfiguration<IAsyncQueryOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncQueryOperator<TEntity>> configuration, Func<IServiceProvider, IQueryPreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IQueryPreprocessor<TEntity>>();
            }

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

        #endregion

        #region AddOperator

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncAddOperator<TEntity>> configuration, Func<IServiceProvider, IAddPreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddPreprocessor<TEntity>>();
            }

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

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity>> WithValidation<TEntity>(this IOperatorConfiguration<IAsyncAddOperator<TEntity>> configuration, Func<IServiceProvider, IAddValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddValidator<TEntity>>();
            }

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

        #endregion

        #region UpdateOperator

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> configuration, Func<IServiceProvider, IUpdatePreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdatePreprocessor<TEntity>>();
            }

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

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> WithValidation<TEntity>(this IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> configuration, Func<IServiceProvider, IUpdateValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdateValidator<TEntity>>();
            }

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

        #endregion

        #region DeleteOperator

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> WithPreprocessing<TEntity>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> configuration, Func<IServiceProvider, IDeletePreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeletePreprocessor<TEntity>>();
            }

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

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> WithValidation<TEntity>(this IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> configuration, Func<IServiceProvider, IDeleteValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeleteValidator<TEntity>>();
            }

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

        #endregion

        public static void AddTransient<TOperator>(this IOperatorConfiguration<TOperator> configuration, IServiceCollection services)
            where TOperator : class
        {
            services.AddTransient<TOperator>(configuration.Build);
        }

        public static void AddScoped<TOperator>(this IOperatorConfiguration<TOperator> configuration, IServiceCollection services)
            where TOperator : class
        {
            services.AddScoped<TOperator>(configuration.Build);
        }

        public static void AddSingleton<TOperator>(this IOperatorConfiguration<TOperator> configuration, IServiceCollection services)
            where TOperator : class
        {
            services.AddSingleton<TOperator>(configuration.Build);
        }
    }
}
