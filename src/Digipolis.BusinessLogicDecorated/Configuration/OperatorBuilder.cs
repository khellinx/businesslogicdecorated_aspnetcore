using Digipolis.BusinessLogicDecorated.Extensions;
using Digipolis.BusinessLogicDecorated.Inputs;
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

        private IList<IOperatorConfiguration> _configurations;

        public OperatorBuilder()
        {
            _configurations = new List<IOperatorConfiguration>();
        }

        public void SetDefaultAsyncGetOperatorTypes(Type asyncGetOperatorType = null, Type asyncGetOperatorTypeWithCustomInput = null)
        {
            SetDefaultTypes(typeof(IAsyncGetOperator<>), typeof(IAsyncGetOperator<,>), asyncGetOperatorType, asyncGetOperatorTypeWithCustomInput, ref _defaultAsyncGetOperatorType, ref _defaultAsyncGetOperatorTypeWithCustomInput);
        }

        public void SetDefaultAsyncQueryOperatorTypes(Type asyncQueryOperatorType = null, Type asyncQueryOperatorTypeWithCustomInput = null)
        {
            SetDefaultTypes(typeof(IAsyncQueryOperator<>), typeof(IAsyncQueryOperator<,>), asyncQueryOperatorType, asyncQueryOperatorTypeWithCustomInput, ref _defaultAsyncQueryOperatorType, ref _defaultAsyncQueryOperatorTypeWithCustomInput);
        }

        public void SetDefaultAsyncAddOperatorTypes(Type asyncAddOperatorType = null, Type asyncAddOperatorTypeWithCustomInput = null)
        {
            SetDefaultTypes(typeof(IAsyncAddOperator<>), typeof(IAsyncAddOperator<,>), asyncAddOperatorType, asyncAddOperatorTypeWithCustomInput, ref _defaultAsyncAddOperatorType, ref _defaultAsyncAddOperatorTypeWithCustomInput);
        }

        public void SetDefaultAsyncUpdateOperatorTypes(Type asyncUpdateOperatorType = null, Type asyncUpdateOperatorTypeWithCustomInput = null)
        {
            SetDefaultTypes(typeof(IAsyncUpdateOperator<>), typeof(IAsyncUpdateOperator<,>), asyncUpdateOperatorType, asyncUpdateOperatorTypeWithCustomInput, ref _defaultAsyncUpdateOperatorType, ref _defaultAsyncUpdateOperatorTypeWithCustomInput);
        }

        public void SetDefaultAsyncDeleteOperatorTypes(Type asyncDeleteOperatorType = null, Type asyncDeleteOperatorTypeWithCustomInput = null)
        {
            SetDefaultTypes(typeof(IAsyncDeleteOperator<>), typeof(IAsyncDeleteOperator<,>), asyncDeleteOperatorType, asyncDeleteOperatorTypeWithCustomInput, ref _defaultAsyncDeleteOperatorType, ref _defaultAsyncDeleteOperatorTypeWithCustomInput);
        }

        private void SetDefaultTypes(Type interfaceType, Type interfaceTypeWithCustomInput, Type operatorType, Type operatorTypeWithCustomInput, ref Type defaultType, ref Type defaultTypeWithCustomInput)
        {
            if (operatorType != null && !operatorType.IsAssignableToGenericType(interfaceType))
            {
                throw new ArgumentException($"The specified type does not implement interface {interfaceType.Name}.");
            }

            if (operatorTypeWithCustomInput != null && !operatorTypeWithCustomInput.IsAssignableToGenericType(interfaceTypeWithCustomInput))
            {
                throw new ArgumentException($"The specified type does not implement interface {interfaceTypeWithCustomInput.Name}.");
            }

            if (operatorType != null)
            {
                defaultType = operatorType;
            }
            if (operatorTypeWithCustomInput != null)
            {
                defaultTypeWithCustomInput = operatorTypeWithCustomInput;
            }
        }

        public IOperatorConfigurationCrudCollection<TEntity, QueryInput<TEntity>, GetInput<TEntity>> ConfigureAsyncCrudOperators<TEntity>()
        {
            return ConfigureAsyncCrudOperators<TEntity, QueryInput<TEntity>, GetInput<TEntity>>();
        }

        public IOperatorConfigurationCrudCollection<TEntity, TQueryInput, GetInput<TEntity>> ConfigureAsyncCrudOperators<TEntity, TQueryInput>()
            where TQueryInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
        {
            return ConfigureAsyncCrudOperators<TEntity, TQueryInput, GetInput<TEntity>>();
        }

        public IOperatorConfigurationCrudCollection<TEntity, TQueryInput, TGetInput> ConfigureAsyncCrudOperators<TEntity, TQueryInput, TGetInput>()
            where TGetInput : IHasIncludes<TEntity>
            where TQueryInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
        {
            if (_defaultAsyncGetOperatorType == null)
            {
                throw new InvalidOperationException("There is no default Get operator implementation specified. All operator types should have a default implementation in order to use the CRUD configuration.");
            }
            if (_defaultAsyncQueryOperatorType == null)
            {
                throw new InvalidOperationException("There is no default Query operator implementation specified. All operator types should have a default implementation in order to use the CRUD configuration.");
            }
            if (_defaultAsyncAddOperatorType == null)
            {
                throw new InvalidOperationException("There is no default Add operator implementation specified. All operator types should have a default implementation in order to use the CRUD configuration.");
            }
            if (_defaultAsyncUpdateOperatorType == null)
            {
                throw new InvalidOperationException("There is no default Update operator implementation specified. All operator types should have a default implementation in order to use the CRUD configuration.");
            }
            if (_defaultAsyncDeleteOperatorType == null)
            {
                throw new InvalidOperationException("There is no default Delete operator implementation specified. All operator types should have a default implementation in order to use the CRUD configuration.");
            }

            var getOperatorConfig = CreateConfiguration<TEntity, IAsyncGetOperator<TEntity, TGetInput>>(null, _defaultAsyncGetOperatorType);
            var queryOperatorConfig = CreateConfiguration<TEntity, IAsyncQueryOperator<TEntity, TQueryInput>>(null, _defaultAsyncQueryOperatorType);
            var addOperatorConfig = CreateConfiguration<TEntity, IAsyncAddOperator<TEntity>>(null, _defaultAsyncAddOperatorType);
            var updateOperatorConfig = CreateConfiguration<TEntity, IAsyncUpdateOperator<TEntity>>(null, _defaultAsyncUpdateOperatorType);
            var deleteOperatorConfig = CreateConfiguration<TEntity, IAsyncDeleteOperator<TEntity>>(null, _defaultAsyncDeleteOperatorType);

            var result = new OperatorConfigurationCrudCollection<TEntity, TQueryInput, TGetInput>(getOperatorConfig, queryOperatorConfig, addOperatorConfig, updateOperatorConfig, deleteOperatorConfig);

            return result;
        }

        public IOperatorConfiguration<IAsyncGetOperator<TEntity>> ConfigureAsyncGetOperator<TEntity>(Func<IServiceProvider, IAsyncGetOperator<TEntity>> operatorFactory = null)
        {
            return CreateConfiguration<TEntity, IAsyncGetOperator<TEntity>>(operatorFactory, _defaultAsyncGetOperatorType);
        }

        public IOperatorConfiguration<IAsyncGetOperator<TEntity, TInput>> ConfigureAsyncGetOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncGetOperator<TEntity, TInput>> operatorFactory = null)
            where TInput : IHasIncludes<TEntity>
        {
            return CreateConfiguration<TEntity, TInput, IAsyncGetOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncGetOperatorTypeWithCustomInput);
        }

        public IOperatorConfiguration<IAsyncQueryOperator<TEntity>> ConfigureAsyncQueryOperator<TEntity>(Func<IServiceProvider, IAsyncQueryOperator<TEntity>> operatorFactory = null)
        {
            return CreateConfiguration<TEntity, IAsyncQueryOperator<TEntity>>(operatorFactory, _defaultAsyncQueryOperatorType);
        }

        public IOperatorConfiguration<IAsyncQueryOperator<TEntity, TInput>> ConfigureAsyncQueryOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> operatorFactory = null)
            where TInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
        {
            return CreateConfiguration<TEntity, TInput, IAsyncQueryOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncQueryOperatorTypeWithCustomInput);
        }

        public IOperatorConfiguration<IAsyncAddOperator<TEntity>> ConfigureAsyncAddOperator<TEntity>(Func<IServiceProvider, IAsyncAddOperator<TEntity>> operatorFactory = null)
        {
            return CreateConfiguration<TEntity, IAsyncAddOperator<TEntity>>(operatorFactory, _defaultAsyncAddOperatorType);
        }

        public IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>> ConfigureAsyncAddOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncAddOperator<TEntity, TInput>> operatorFactory = null)
        {
            return CreateConfiguration<TEntity, TInput, IAsyncAddOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncAddOperatorTypeWithCustomInput);
        }

        public IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> ConfigureAsyncUpdateOperator<TEntity>(Func<IServiceProvider, IAsyncUpdateOperator<TEntity>> operatorFactory = null)
        {
            return CreateConfiguration<TEntity, IAsyncUpdateOperator<TEntity>>(operatorFactory, _defaultAsyncUpdateOperatorType);
        }

        public IOperatorConfiguration<IAsyncUpdateOperator<TEntity, TInput>> ConfigureAsyncUpdateOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, TInput>> operatorFactory = null)
        {
            return CreateConfiguration<TEntity, TInput, IAsyncUpdateOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncUpdateOperatorTypeWithCustomInput);
        }

        public IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> ConfigureAsyncDeleteOperator<TEntity>(Func<IServiceProvider, IAsyncDeleteOperator<TEntity>> operatorFactory = null)
        {
            return CreateConfiguration<TEntity, IAsyncDeleteOperator<TEntity>>(operatorFactory, _defaultAsyncDeleteOperatorType);
        }

        public IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>> ConfigureAsyncDeleteOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TInput>> operatorFactory = null)
        {
            return CreateConfiguration<TEntity, TInput, IAsyncDeleteOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncDeleteOperatorTypeWithCustomInput);
        }

        private IOperatorConfiguration<TOperator> CreateConfiguration<TEntity, TOperator>(Func<IServiceProvider, TOperator> operatorFactory, Type defaultType)
            where TOperator : class
        {
            if (operatorFactory == null && defaultType == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory), "When the default type is not set, the operator factory is required.");
            }

            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, defaultType.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity)));
                    return result as TOperator;
                };
            }

            var configuration = new OperatorConfiguration<TOperator>(operatorFactory);
            _configurations.Add(configuration);

            return configuration;
        }

        private IOperatorConfiguration<TOperator> CreateConfiguration<TEntity, TInput, TOperator>(Func<IServiceProvider, TOperator> operatorFactory, Type defaultType)
            where TOperator : class
        {
            if (operatorFactory == null && defaultType == null)
            {
                throw new ArgumentNullException(nameof(operatorFactory), "When the default type is not set, the operator factory is required.");
            }

            if (operatorFactory == null)
            {
                operatorFactory = serviceProvider =>
                {
                    var result = ActivatorUtilities.CreateInstance(serviceProvider, defaultType.GetGenericTypeDefinition().MakeGenericType(typeof(TEntity), typeof(TInput)));
                    return result as TOperator;
                };
            }

            var configuration = new OperatorConfiguration<TOperator>(operatorFactory);
            _configurations.Add(configuration);

            return configuration;
        }

        public void AddOperators(IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            foreach (var conf in _configurations)
            {
                services.Add(new ServiceDescriptor(conf.OperatorType, conf.Build, lifetime));
            }
        }
    }
}
