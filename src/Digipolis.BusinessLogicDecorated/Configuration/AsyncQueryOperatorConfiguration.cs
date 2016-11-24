using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public class AsyncQueryOperatorConfiguration<TEntity> : AsyncQueryOperatorConfiguration<TEntity, QueryInput<TEntity>>
    {
        public AsyncQueryOperatorConfiguration(Func<IServiceProvider, IAsyncQueryOperator<TEntity, QueryInput<TEntity>>> operatorFactory) : base(operatorFactory)
        {
        }
    }

    public class AsyncQueryOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>>, IAsyncQueryOperatorConfiguration<TEntity, TInput>
        where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
    {
        public AsyncQueryOperatorConfiguration(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>> WithPreprocessing(Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IQueryPreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IQueryPreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IQueryPreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncQueryPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }
    }
}
