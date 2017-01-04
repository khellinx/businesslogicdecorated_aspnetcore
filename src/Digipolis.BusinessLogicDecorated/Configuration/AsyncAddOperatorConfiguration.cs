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
    public class AsyncAddOperatorConfiguration<TEntity> : OperatorConfiguration<IAsyncAddOperator<TEntity>>, IAsyncAddOperatorConfiguration<TEntity>
    {
        public AsyncAddOperatorConfiguration(Func<IServiceProvider, IAsyncAddOperator<TEntity>> operatorFactory) : base(operatorFactory)
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

        public IAsyncAddOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IAddPostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncAddPostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAddPostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncAddPostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncAddPostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncAddPostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncAddPostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncAddPostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncAddPreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncAddPreprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncAddPreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncAddPreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncAddPreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncAddPreprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IAddValidator<TEntity>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncAddValidationDecorator<TEntity>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncAddValidationDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithAsyncValidation(Func<IServiceProvider, IAsyncAddValidator<TEntity>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncAddValidationDecorator<TEntity>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncAddValidator<TEntity>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncAddValidationDecorator<TEntity>(op, dep));

            return this;
        }
    }

    public class AsyncAddOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncAddOperator<TEntity, TInput>>, IAsyncAddOperatorConfiguration<TEntity, TInput>
    {
        public AsyncAddOperatorConfiguration(Func<IServiceProvider, IAsyncAddOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
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

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IAddPostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncAddPostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAddPostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncAddPostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncAddPostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncAddPostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncAddPostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncAddPostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IAddPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAddPreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncAddPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncAddPreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncAddPreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IAddValidator<TEntity, TInput>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncAddValidationDecorator<TEntity, TInput>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity, TInput>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncAddValidationDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncValidation(Func<IServiceProvider, IAsyncAddValidator<TEntity, TInput>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncAddValidationDecorator<TEntity, TInput>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncAddValidator<TEntity, TInput>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncAddValidationDecorator<TEntity, TInput>(op, dep));

            return this;
        }
    }
}
