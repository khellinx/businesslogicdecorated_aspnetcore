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
    public class AsyncUpdateOperatorConfiguration<TEntity> : OperatorConfiguration<IAsyncUpdateOperator<TEntity>>, IAsyncUpdateOperatorConfiguration<TEntity>
    {
        public AsyncUpdateOperatorConfiguration(Func<IServiceProvider, IAsyncUpdateOperator<TEntity>> operatorFactory) : base(typeof(TEntity), operatorFactory)
        {
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncUpdateOperator<TEntity>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncUpdateOperator<TEntity>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IUpdatePostprocessor<TEntity>> PostprocessorFactory = null)
        {
            if (PostprocessorFactory == null)
            {
                PostprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdatePostprocessor<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePostprocessingDecorator<TEntity>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IUpdatePostprocessor<TEntity>
        {
            Func<IServiceProvider, IUpdatePostprocessor<TEntity>> PostprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPostprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePostprocessingDecorator<TEntity>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IUpdatePreprocessor<TEntity>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdatePreprocessor<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IUpdatePreprocessor<TEntity>
        {
            Func<IServiceProvider, IUpdatePreprocessor<TEntity>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IUpdateValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdateValidator<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IUpdateValidator<TEntity>
        {
            Func<IServiceProvider, IUpdateValidator<TEntity>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncValidation(Func<IServiceProvider, IAsyncUpdateValidator<TEntity>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IAsyncUpdateValidator<TEntity>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncUpdateValidator<TEntity>
        {
            Func<IServiceProvider, IAsyncUpdateValidator<TEntity>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity>(op, validatorFactory(serviceProvider)));

            return this;
        }
    }

    public class AsyncUpdateOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>>, IAsyncUpdateOperatorConfiguration<TEntity, TInput>
    {
        public AsyncUpdateOperatorConfiguration(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, TInput>> operatorFactory) : base(typeof(TEntity), operatorFactory)
        {
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, TInput>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncUpdateOperator<TEntity, TInput>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IUpdatePostprocessor<TEntity, TInput>> PostprocessorFactory = null)
        {
            if (PostprocessorFactory == null)
            {
                PostprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdatePostprocessor<TEntity, TInput>>();
            }

            SurroundWithDecorator((op, serviceProvider) => new AsyncUpdatePostprocessingDecorator<TEntity, TInput>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IUpdatePostprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IUpdatePostprocessor<TEntity, TInput>> PostprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPostprocessor>(serviceProvider);
            };

            SurroundWithDecorator((op, serviceProvider) => new AsyncUpdatePostprocessingDecorator<TEntity, TInput>(op, PostprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdatePreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IUpdatePreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IUpdateValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IUpdateValidator<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IUpdateValidator<TEntity, TInput>
        {
            Func<IServiceProvider, IUpdateValidator<TEntity, TInput>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncValidation(Func<IServiceProvider, IAsyncUpdateValidator<TEntity, TInput>> validatorFactory = null)
        {
            if (validatorFactory == null)
            {
                validatorFactory = serviceProvider => serviceProvider.GetRequiredService<IAsyncUpdateValidator<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncUpdateValidator<TEntity, TInput>
        {
            Func<IServiceProvider, IAsyncUpdateValidator<TEntity, TInput>> validatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, validatorFactory(serviceProvider)));

            return this;
        }
    }
}
