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
    public class AsyncAddOperatorConfiguration<TEntity> : AsyncAddOperatorConfiguration<TEntity, object>
    {
        public AsyncAddOperatorConfiguration(Func<IServiceProvider, IAsyncAddOperator<TEntity, object>> operatorFactory) : base(operatorFactory)
        {
        }
    }

    public class AsyncAddOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncAddOperator<TEntity, TInput>>, IAsyncAddOperatorConfiguration<TEntity, TInput>
    {
        public AsyncAddOperatorConfiguration(Func<IServiceProvider, IAsyncAddOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddPreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> WithValidation(Func<IServiceProvider, IAddValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IAddValidator<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncAddValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> WithValidation<TValidator>()
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
