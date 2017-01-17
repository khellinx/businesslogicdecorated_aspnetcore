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
        public AsyncDeleteOperatorConfiguration(Func<IServiceProvider, IAsyncDeleteOperator<TEntity>> operatorFactory) : base(operatorFactory)
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

        public IAsyncDeleteOperatorConfiguration<TEntity> WithPostprocessing(Func<IServiceProvider, IDeletePostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IDeletePostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncDeletePostprocessor<TEntity>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncDeletePostprocessor<TEntity>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncDeletePreprocessor<TEntity>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncDeletePreprocessor<TEntity>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeleteValidationDecorator<TEntity>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithValidation<TValidator>()
            where TValidator : class, IDeleteValidator<TEntity>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncDeleteValidationDecorator<TEntity>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncValidation(Func<IServiceProvider, IAsyncDeleteValidator<TEntity>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeleteValidationDecorator<TEntity>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncDeleteValidator<TEntity>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncDeleteValidationDecorator<TEntity>(op, dep));

            return this;
        }
    }

    public class AsyncDeleteOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>>, IAsyncDeleteOperatorConfiguration<TEntity, TInput>
    {
        public AsyncDeleteOperatorConfiguration(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
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

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPostprocessing(Func<IServiceProvider, IDeletePostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IDeletePostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncDeletePostprocessor<TEntity, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncDeletePostprocessor<TEntity, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncDeletePreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncDeletePreprocessor<TEntity, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity, TInput>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeleteValidationDecorator<TEntity, TInput>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithValidation<TValidator>()
            where TValidator : class, IDeleteValidator<TEntity, TInput>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncDeleteValidationDecorator<TEntity, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncValidation(Func<IServiceProvider, IAsyncDeleteValidator<TEntity, TInput>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeleteValidationDecorator<TEntity, TInput>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncDeleteValidator<TEntity, TInput>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncDeleteValidationDecorator<TEntity, TInput>(op, dep));

            return this;
        }
    }

    public class AsyncDeleteOperatorConfiguration<TEntity, TId, TInput> : OperatorConfiguration<IAsyncDeleteOperator<TEntity, TId, TInput>>, IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput>
    {
        public AsyncDeleteOperatorConfiguration(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TId, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithCustomOperator(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TId, TInput>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory));
            }

            OperatorFactory = operatorFactory;

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithCustomOperator<TCustomOperator>()
            where TCustomOperator : class, IAsyncDeleteOperator<TEntity, TId, TInput>
        {
            OperatorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TCustomOperator>(serviceProvider);
            };

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithPostprocessing(Func<IServiceProvider, IDeletePostprocessor<TEntity, TId, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity, TId, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IDeletePostprocessor<TEntity, TId, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithAsyncPostprocessing(Func<IServiceProvider, IAsyncDeletePostprocessor<TEntity, TId, TInput>> postprocessorFactory = null)
        {
            AppendDecorator((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity, TId, TInput>(op, dep), postprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithAsyncPostprocessing<TPostprocessor>()
            where TPostprocessor : class, IAsyncDeletePostprocessor<TEntity, TId, TInput>
        {
            AppendDecorator<TPostprocessor>((op, dep) => new AsyncDeletePostprocessingDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithPreprocessing(Func<IServiceProvider, IDeletePreprocessor<TEntity, TId, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity, TId, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IDeletePreprocessor<TEntity, TId, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithAsyncPreprocessing(Func<IServiceProvider, IAsyncDeletePreprocessor<TEntity, TId, TInput>> preprocessorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity, TId, TInput>(op, dep), preprocessorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithAsyncPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IAsyncDeletePreprocessor<TEntity, TId, TInput>
        {
            InsertDecorator<TPreprocessor>((op, dep) => new AsyncDeletePreprocessingDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithValidation(Func<IServiceProvider, IDeleteValidator<TEntity, TId, TInput>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeleteValidationDecorator<TEntity, TId, TInput>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithValidation<TValidator>()
            where TValidator : class, IDeleteValidator<TEntity, TId, TInput>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncDeleteValidationDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithAsyncValidation(Func<IServiceProvider, IAsyncDeleteValidator<TEntity, TId, TInput>> validatorFactory = null)
        {
            InsertDecorator((op, dep) => new AsyncDeleteValidationDecorator<TEntity, TId, TInput>(op, dep), validatorFactory);

            return this;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TId, TInput> WithAsyncValidation<TValidator>()
            where TValidator : class, IAsyncDeleteValidator<TEntity, TId, TInput>
        {
            InsertDecorator<TValidator>((op, dep) => new AsyncDeleteValidationDecorator<TEntity, TId, TInput>(op, dep));

            return this;
        }
    }
}
