using Digipolis.BusinessLogicDecorated.Extensions;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public class OperatorBuilder
    {
        private Type _defaultAsyncGetOperatorType;
        private Type _defaultAsyncGetOperatorTypeWithCustomInput;
        private Type _defaultAsyncQueryOperatorType;
        private Type _defaultAsyncQueryOperatorTypeWithCustomInput;
        private Type _defaultAsyncAddOperatorType;
        private Type _defaultAsyncAddOperatorTypeWithCustomInput;
        private Type _defaultAsyncUpdateOperatorType;
        private Type _defaultAsyncUpdateOperatorTypeWithCustomInput;
        private Type _defaultAsyncDeleteOperatorType;
        private Type _defaultAsyncDeleteOperatorTypeWithCustomInput;

        public void SetDefaultAsyncGetOperatorTypes(Type asyncGetOperatorType, Type asyncGetOperatorTypeWithCustomInput)
        {
            if (!asyncGetOperatorType.IsAssignableToGenericType(typeof(IAsyncGetOperator<>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncGetOperator<,>.");
            }

            if (!asyncGetOperatorTypeWithCustomInput.IsAssignableToGenericType(typeof(IAsyncGetOperator<,>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncGetOperator<,>.");
            }

            _defaultAsyncGetOperatorType = asyncGetOperatorType;
            _defaultAsyncGetOperatorTypeWithCustomInput = asyncGetOperatorTypeWithCustomInput;
        }

        public void SetDefaultAsyncQueryOperatorTypes(Type asyncQueryOperatorType, Type asyncQueryOperatorTypeWithCustomInput)
        {
            if (!asyncQueryOperatorType.IsAssignableToGenericType(typeof(IAsyncQueryOperator<>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncGetOperator<,>.");
            }

            if (!asyncQueryOperatorTypeWithCustomInput.IsAssignableToGenericType(typeof(IAsyncQueryOperator<,>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncGetOperator<,>.");
            }

            _defaultAsyncQueryOperatorType = asyncQueryOperatorType;
            _defaultAsyncQueryOperatorTypeWithCustomInput = asyncQueryOperatorTypeWithCustomInput;
        }

        public void SetDefaultAsyncAddOperatorTypes(Type asyncAddOperatorType, Type asyncAddOperatorTypeWithCustomInput)
        {
            if (!asyncAddOperatorType.IsAssignableToGenericType(typeof(IAsyncAddOperator<>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncAddOperator<,>.");
            }

            if (!asyncAddOperatorTypeWithCustomInput.IsAssignableToGenericType(typeof(IAsyncAddOperator<,>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncAddOperator<,>.");
            }

            _defaultAsyncAddOperatorType = asyncAddOperatorType;
            _defaultAsyncAddOperatorTypeWithCustomInput = asyncAddOperatorTypeWithCustomInput;
        }

        public void SetDefaultAsyncUpdateOperatorTypes(Type asyncUpdateOperatorType, Type asyncUpdateOperatorTypeWithCustomInput)
        {
            if (!asyncUpdateOperatorType.IsAssignableToGenericType(typeof(IAsyncUpdateOperator<>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncUpdateOperator<,>.");
            }

            if (!asyncUpdateOperatorTypeWithCustomInput.IsAssignableToGenericType(typeof(IAsyncUpdateOperator<,>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncUpdateOperator<,>.");
            }

            _defaultAsyncUpdateOperatorType = asyncUpdateOperatorType;
            _defaultAsyncUpdateOperatorTypeWithCustomInput = asyncUpdateOperatorTypeWithCustomInput;
        }

        public void SetDefaultAsyncDeleteOperatorTypes(Type asyncDeleteOperatorType, Type asyncDeleteOperatorTypeWithCustomInput)
        {
            if (!asyncDeleteOperatorType.IsAssignableToGenericType(typeof(IAsyncDeleteOperator<>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncDeleteOperator<,>.");
            }

            if (!asyncDeleteOperatorTypeWithCustomInput.IsAssignableToGenericType(typeof(IAsyncDeleteOperator<,>)))
            {
                throw new ArgumentException("The specified type does not implement interface IAsyncDeleteOperator<,>.");
            }

            _defaultAsyncDeleteOperatorType = asyncDeleteOperatorType;
            _defaultAsyncDeleteOperatorTypeWithCustomInput = asyncDeleteOperatorTypeWithCustomInput;
        }

        public IOperatorConfiguration<IAsyncGetOperator<TEntity>> ConfigureAsyncGetOperator<TEntity>(Func<IServiceProvider, IAsyncGetOperator<TEntity>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncGetOperatorType.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity)));
                    return result as IAsyncGetOperator<TEntity>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncGetOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>> ConfigureAsyncGetOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncGetOperator<TEntity, TInput>> operatorFactory = null)
            where TInput : IHasIncludes<TEntity>
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncGetOperatorTypeWithCustomInput.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity), typeof(TInput)));
                    return result as IAsyncGetOperator<TEntity, TInput>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncGetOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }

        public IOperatorConfiguration<IAsyncQueryOperator<TEntity>> ConfigureAsyncQueryOperator<TEntity>(Func<IServiceProvider, IAsyncQueryOperator<TEntity>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncQueryOperatorType.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity)));
                    return result as IAsyncQueryOperator<TEntity>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncQueryOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>> ConfigureAsyncQueryOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> operatorFactory = null)
            where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncQueryOperatorTypeWithCustomInput.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity), typeof(TInput)));
                    return result as IAsyncQueryOperator<TEntity, TInput>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }

        public IOperatorConfiguration<IAsyncAddOperator<TEntity>> ConfigureAsyncAddOperator<TEntity>(Func<IServiceProvider, IAsyncAddOperator<TEntity>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncAddOperatorType.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity)));
                    return result as IAsyncAddOperator<TEntity>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncAddOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> ConfigureAsyncAddOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncAddOperator<TEntity, TInput>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncAddOperatorTypeWithCustomInput.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity), typeof(TInput)));
                    return result as IAsyncAddOperator<TEntity, TInput>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncAddOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }

        public IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> ConfigureAsyncUpdateOperator<TEntity>(Func<IServiceProvider, IAsyncUpdateOperator<TEntity>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncUpdateOperatorType.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity)));
                    return result as IAsyncUpdateOperator<TEntity>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncUpdateOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> ConfigureAsyncUpdateOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, TInput>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncUpdateOperatorTypeWithCustomInput.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity), typeof(TInput)));
                    return result as IAsyncUpdateOperator<TEntity, TInput>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }

        public IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> ConfigureAsyncDeleteOperator<TEntity>(Func<IServiceProvider, IAsyncDeleteOperator<TEntity>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncDeleteOperatorType.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity)));
                    return result as IAsyncDeleteOperator<TEntity>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncDeleteOperator<TEntity>>(operatorFactory);

            return configuration;
        }

        public IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> ConfigureAsyncDeleteOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TInput>> operatorFactory = null)
        {
            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, _defaultAsyncDeleteOperatorTypeWithCustomInput.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity), typeof(TInput)));
                    return result as IAsyncDeleteOperator<TEntity, TInput>;
                };
            }

            var configuration = new OperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>>(operatorFactory);

            return configuration;
        }

        
    }
}
