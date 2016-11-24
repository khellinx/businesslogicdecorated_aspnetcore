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
    public class AsyncDeleteOperatorConfiguration<TEntity> : AsyncDeleteOperatorConfiguration<TEntity, object>
    {
        public AsyncDeleteOperatorConfiguration(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, object>> operatorFactory) : base(operatorFactory)
        {
        }
    }

    public class AsyncDeleteOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>>, IAsyncDeleteOperatorConfiguration<TEntity, TInput>
    {
        public AsyncDeleteOperatorConfiguration(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeletePreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IDeletePreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeleteValidator<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeleteValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> WithValidation<TValidator>()
            where TValidator : class, IDeleteValidator<TEntity, TInput>
        {
            Func<IServiceProvider, IDeleteValidator<TEntity, TInput>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeleteValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }
    }
}
