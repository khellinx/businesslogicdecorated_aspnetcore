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
    public class AsyncGetOperatorConfiguration<TEntity> : AsyncGetOperatorConfiguration<TEntity, GetInput<TEntity>>
    {
        public AsyncGetOperatorConfiguration(Func<IServiceProvider, IAsyncGetOperator<TEntity, GetInput<TEntity>>> operatorFactory) : base(operatorFactory)
        {
        }
    }

    public class AsyncGetOperatorConfiguration<TEntity, TInput> : OperatorConfiguration<IAsyncGetOperator<TEntity, TInput>>, IAsyncGetOperatorConfiguration<TEntity, TInput>
        where TInput : IHasIncludes<TEntity>
    {
        public AsyncGetOperatorConfiguration(Func<IServiceProvider, IAsyncGetOperator<TEntity, TInput>> operatorFactory) : base(operatorFactory)
        {
        }

        public IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>> WithPreprocessing(Func<IServiceProvider, IGetPreprocessor<TEntity, TInput>> preprocessorFactory = null)
        {
            if (preprocessorFactory == null)
            {
                preprocessorFactory = serviceProvider => serviceProvider.GetRequiredService<IGetPreprocessor<TEntity, TInput>>();
            }

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }

        public IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class, IGetPreprocessor<TEntity, TInput>
        {
            Func<IServiceProvider, IGetPreprocessor<TEntity, TInput>> preprocessorFactory = serviceProvider =>
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<TPreprocessor>(serviceProvider);
            };

            InsertDecoratorBeforeOperator((op, serviceProvider) => new AsyncGetPreprocessingDecorator<TEntity, TInput>(op, preprocessorFactory(serviceProvider)));

            return this;
        }
    }
}
