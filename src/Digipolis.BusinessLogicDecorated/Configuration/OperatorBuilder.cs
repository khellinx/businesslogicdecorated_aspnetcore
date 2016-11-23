using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public class OperatorBuilder
    {
        public static IOperatorConfiguration<IAsyncGetOperator<TEntity>> AddAsyncGetOperator<TEntity, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TOperator : class, IAsyncGetOperator<TEntity>
        {
            var configuration = new OperatorConfiguration<IAsyncGetOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>> AddAsyncGetOperator<TEntity, TInput, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TInput : IHasIncludes<TEntity>
            where TOperator : class, IAsyncGetOperator<TEntity, TInput>
        {
            var configuration = new OperatorConfiguration<IAsyncGetOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncQueryOperator<TEntity>> AddAsyncQueryOperator<TEntity, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TOperator : class, IAsyncQueryOperator<TEntity>
        {
            var configuration = new OperatorConfiguration<IAsyncQueryOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>> AddAsyncQueryOperator<TEntity, TInput, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
            where TOperator : class, IAsyncQueryOperator<TEntity, TInput>
        {
            var configuration = new OperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity>> AddAsyncAddOperator<TEntity, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TOperator : class, IAsyncAddOperator<TEntity>
        {
            var configuration = new OperatorConfiguration<IAsyncAddOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> AddAsyncAddOperator<TEntity, TInput, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TOperator : class, IAsyncAddOperator<TEntity, TInput>
        {
            var configuration = new OperatorConfiguration<IAsyncAddOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> AddAsyncUpdateOperator<TEntity, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TOperator : class, IAsyncUpdateOperator<TEntity>
        {
            var configuration = new OperatorConfiguration<IAsyncUpdateOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> AddAsyncUpdateOperator<TEntity, TInput, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TOperator : class, IAsyncUpdateOperator<TEntity, TInput>
        {
            var configuration = new OperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> AddAsyncDeleteOperator<TEntity, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TOperator : class, IAsyncDeleteOperator<TEntity>
        {
            var configuration = new OperatorConfiguration<IAsyncDeleteOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public static IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> AddAsyncDeleteOperator<TEntity, TInput, TOperator>(Func<IServiceProvider, TOperator> operatorFactory)
            where TOperator : class, IAsyncDeleteOperator<TEntity, TInput>
        {
            var configuration = new OperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }
    }
}
