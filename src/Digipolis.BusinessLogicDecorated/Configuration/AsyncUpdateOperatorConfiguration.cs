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
    public class AsyncUpdateOperatorConfiguration<TEntity> : AsyncUpdateOperatorConfiguration<TEntity, object>
    {
        public AsyncUpdateOperatorConfiguration(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, object>> operatorFactory) : base(operatorFactory)
        {
        }
    }

    public class AsyncUpdateOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>>, IAsyncUpdateOperatorConfiguration<TEntity, TInput>
    {
        public AsyncUpdateOperatorConfiguration(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> WithPreprocessing(Func<IServiceProvider, IUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdatePreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IUpdatePreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> WithValidation(Func<IServiceProvider, IUpdateValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdateValidator<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> WithValidation<TValidator>()
            where TValidator : class, IUpdateValidator<TEntity, TInput>
        {
            Func<IServiceProvider, IUpdateValidator<TEntity, TInput>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }
    }
}
