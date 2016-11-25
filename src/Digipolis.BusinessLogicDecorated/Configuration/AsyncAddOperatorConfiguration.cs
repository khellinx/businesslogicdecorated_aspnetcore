using Digipolis.BusinessLogicDecorated.Decorators;
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
    public class AsyncAddOperatorConfiguration<TEntity> : OperatorConfiguration<IAsyncAddOperator<TEntity>>, IAsyncAddOperatorConfiguration<TEntity>
    {
        public AsyncAddOperatorConfiguration(Func<IServiceProvider, IAsyncAddOperator<TEntity>> operatorFactory) : base(typeof(TEntity), operatorFactory)
        {
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncAddOperator<TEntity>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncAddOperator<TEntity>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddPreprocessor<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity>
        {
            Func<IServiceProvider, IAddPreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IAddValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddValidator<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity>
        {
            Func<IServiceProvider, IAddValidator<TEntity>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return this;
        }
    }

    public class AsyncAddOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncAddOperator<TEntity, TInput>>, IAsyncAddOperatorConfiguration<TEntity, TInput>
    {
        public AsyncAddOperatorConfiguration(Func<IServiceProvider, IAsyncAddOperator<TEntity, TInput>> operatorFactory) : base(typeof(TEntity), operatorFactory)
        {
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncAddOperator<TEntity, TInput>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncAddOperator<TEntity, TInput>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddPreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IAddValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddValidator<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity, TInput>
        {
            Func<IServiceProvider, IAddValidator<TEntity, TInput>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }
    }
}
