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
        public AsyncUpdateOperatorConfiguration(Func<IServiceProvider, IAsyncUpdateOperator<TEntity>> operatorFactory) : base(operatorFactory)
        {
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithCustomOperator(Func<IServiceProvider, IAsyncUpdateOperator<TEntity>> operatorFactory)
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

        public IAsyncUpdateOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IUpdatePostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncUpdatePostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IUpdatePostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncUpdatePostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncUpdatePostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncUpdatePostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncUpdatePostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncUpdatePostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IUpdatePreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncUpdatePreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IUpdatePreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncUpdatePreprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncUpdatePreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncUpdatePreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncUpdatePreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncUpdatePreprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IUpdateValidator<TEntity>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncUpdateValidationDecorator<TEntity>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IUpdateValidator<TEntity>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncUpdateValidationDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncValidation(Func<IServiceProvider, IAsyncUpdateValidator<TEntity>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncUpdateValidationDecorator<TEntity>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncUpdateValidator<TEntity>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncUpdateValidationDecorator<TEntity>(op, dep));

            return this;
        }
    }

    public class AsyncUpdateOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>>, IAsyncUpdateOperatorConfiguration<TEntity, TInput>
    {
        public AsyncUpdateOperatorConfiguration(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, TInput>> operatorFactory)
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

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IUpdatePostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncUpdatePostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IUpdatePostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncUpdatePostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncUpdatePostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncUpdatePostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncUpdatePostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncUpdatePostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IUpdatePreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncUpdatePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncUpdatePreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncUpdatePreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IUpdateValidator<TEntity, TInput>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IUpdateValidator<TEntity, TInput>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncValidation(Func<IServiceProvider, IAsyncUpdateValidator<TEntity, TInput>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncUpdateValidator<TEntity, TInput>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncUpdateValidationDecorator<TEntity, TInput>(op, dep));

            return this;
        }
    }
}
