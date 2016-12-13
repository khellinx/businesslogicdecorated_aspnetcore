using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Digipolis.BusinessLogicDecorated.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public class AsyncDeleteOperatorConfiguration<TEntity> : OperatorConfiguration<IAsyncDeleteOperator<TEntity>>, IAsyncDeleteOperatorConfiguration<TEntity>
    {
        public AsyncDeleteOperatorConfiguration(Func<IServiceProvider, IAsyncDeleteOperator<TEntity>> operatorFactory) : base(typeof(TEntity), operatorFactory)
        {
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncDeleteOperator<TEntity>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncDeleteOperator<TEntity>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IDeletePostprocessor<TEntity>> PostprocessorFactory = null)
        {
            if (PostprocessorFactory == null)
            {
                PostprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeletePostprocessor<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePostprocessingDecorator<TEntity>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IDeletePostprocessor<TEntity>
        {
            Func<IServiceProvider, IDeletePostprocessor<TEntity>> PostprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPostprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePostprocessingDecorator<TEntity>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeletePreprocessor<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity>
        {
            Func<IServiceProvider, IDeletePreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeleteValidator<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeleteValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IDeleteValidator<TEntity>
        {
            Func<IServiceProvider, IDeleteValidator<TEntity>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeleteValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return this;
        }
    }

    public class AsyncDeleteOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>>, IAsyncDeleteOperatorConfiguration<TEntity, TInput>
    {
        public AsyncDeleteOperatorConfiguration(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TInput>> operatorFactory) : base(typeof(TEntity), operatorFactory)
        {
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TInput>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncDeleteOperator<TEntity, TInput>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IDeletePostprocessor<TEntity, TInput>> PostprocessorFactory = null)
        {
            if (PostprocessorFactory == null)
            {
                PostprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeletePostprocessor<TEntity, TInput>>();
            }

            SurroundWithDecorator((op, serviceProvider) => new AsyncDeletePostprocessingDecorator<TEntity, TInput>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IDeletePostprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IDeletePostprocessor<TEntity, TInput>> PostprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPostprocessor>(serviceProvider);
            };

            SurroundWithDecorator((op, serviceProvider) => new AsyncDeletePostprocessingDecorator<TEntity, TInput>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeletePreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IDeletePreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IDeleteValidator<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncDeleteValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
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
