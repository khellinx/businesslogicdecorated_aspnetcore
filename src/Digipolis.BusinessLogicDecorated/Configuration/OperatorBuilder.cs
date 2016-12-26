using Digipolis.BusinessLogicDecorated.Extensions;
using Digipolis.BusinessLogicDecorated.Inputs;
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
        private IList<ServiceDescriptor> _operatorServiceDescriptors;
        private IList<ServiceDescriptor> _crudOperatorCollectionServiceDescriptors;
        private ServiceLifetime _serviceLifetime;

        public OperatorBuilder(ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            _configurations = new List<IOperatorConfiguration>();
            _operatorServiceDescriptors = new List<ServiceDescriptor>();
            _crudOperatorCollectionServiceDescriptors = new List<ServiceDescriptor>();
            _serviceLifetime = lifetime;
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

        public ICrudOperatorConfigurationCollection<TEntity> ConfigureAsyncCrudOperators<TEntity>()
        {
            if (_defaultAsyncGetOperatorTypeWithCustomInput == null)
            {
                throw new InvalidOperationException("There is no default Get operator with custom input implementation specified. All operator types should have a default implementation in order to use the CRUD configuration.");
            }
            if (_defaultAsyncQueryOperatorTypeWithCustomInput == null)
            {
                throw new InvalidOperationException("There is no default Query operator with custom input implementation specified. All operator types should have a default implementation in order to use the CRUD configuration.");
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

            var getOperatorConfig = ConfigureAsyncGetOperator<TEntity, GetInput<TEntity>>();
            var queryOperatorConfig = ConfigureAsyncQueryOperator<TEntity, QueryInput<TEntity>>();
            var addOperatorConfig = ConfigureAsyncAddOperator<TEntity>();
            var updateOperatorConfig = ConfigureAsyncUpdateOperator<TEntity>();
            var deleteOperatorConfig = ConfigureAsyncDeleteOperator<TEntity>();

            var result = new CrudOperatorConfigurationCollection<TEntity>(getOperatorConfig, queryOperatorConfig, addOperatorConfig, updateOperatorConfig, deleteOperatorConfig);

            _crudOperatorCollectionServiceDescriptors.Add(new ServiceDescriptor(typeof(ICrudOperatorCollection<TEntity>), result.BuildSimple, _serviceLifetime));
            _crudOperatorCollectionServiceDescriptors.Add(new ServiceDescriptor(typeof(ICrudOperatorCollection<TEntity, GetInput<TEntity>, QueryInput<TEntity>>), result.Build, _serviceLifetime));

            return result;
        }

        public ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput> ConfigureAsyncCrudOperators<TEntity, TGetInput, TQueryInput>()
            where TGetInput : GetInput<TEntity>
            where TQueryInput : QueryInput<TEntity>
        {
            if (_defaultAsyncGetOperatorTypeWithCustomInput == null)
            {
                throw new InvalidOperationException("There is no default Get operator with custom input implementation specified. All operator types should have a default implementation in order to use the CRUD configuration.");
            }
            if (_defaultAsyncQueryOperatorTypeWithCustomInput == null)
            {
                throw new InvalidOperationException("There is no default Query operator with custom input implementation specified. All operator types should have a default implementation in order to use the CRUD configuration.");
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

            var getOperatorConfig = ConfigureAsyncGetOperator<TEntity, TGetInput>();
            var queryOperatorConfig = ConfigureAsyncQueryOperator<TEntity, TQueryInput>();
            var addOperatorConfig = ConfigureAsyncAddOperator<TEntity>();
            var updateOperatorConfig = ConfigureAsyncUpdateOperator<TEntity>();
            var deleteOperatorConfig = ConfigureAsyncDeleteOperator<TEntity>();

            var result = new CrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput>(getOperatorConfig, queryOperatorConfig, addOperatorConfig, updateOperatorConfig, deleteOperatorConfig);

            _crudOperatorCollectionServiceDescriptors.Add(new ServiceDescriptor(typeof(ICrudOperatorCollection<TEntity, TGetInput, TQueryInput>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncGetOperatorConfiguration<TEntity> ConfigureAsyncGetOperator<TEntity>(Func<IServiceProvider, IAsyncGetOperator<TEntity>> operatorFactory = null)
        {
            operatorFactory = GetOperatorFactory<TEntity, IAsyncGetOperator<TEntity>>(operatorFactory, _defaultAsyncGetOperatorType);

            var result = new AsyncGetOperatorConfiguration<TEntity>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncGetOperator<TEntity>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TInput> ConfigureAsyncGetOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncGetOperator<TEntity, TInput>> operatorFactory = null)
            where TInput : GetInput<TEntity>
        {
            operatorFactory = GetOperatorFactory<TEntity, TInput, IAsyncGetOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncGetOperatorTypeWithCustomInput);

            var result = new AsyncGetOperatorConfiguration<TEntity, TInput>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncGetOperator<TEntity, TInput>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncQueryOperatorConfiguration<TEntity> ConfigureAsyncQueryOperator<TEntity>(Func<IServiceProvider, IAsyncQueryOperator<TEntity>> operatorFactory = null)
        {
            operatorFactory = GetOperatorFactory<TEntity, IAsyncQueryOperator<TEntity>>(operatorFactory, _defaultAsyncQueryOperatorType);

            var result = new AsyncQueryOperatorConfiguration<TEntity>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncQueryOperator<TEntity>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncQueryOperatorConfiguration<TEntity, TInput> ConfigureAsyncQueryOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncQueryOperator<TEntity, TInput>> operatorFactory = null)
            where TInput : QueryInput<TEntity>
        {
            operatorFactory = GetOperatorFactory<TEntity, TInput, IAsyncQueryOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncQueryOperatorTypeWithCustomInput);

            var result = new AsyncQueryOperatorConfiguration<TEntity, TInput>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncQueryOperator<TEntity, TInput>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncAddOperatorConfiguration<TEntity> ConfigureAsyncAddOperator<TEntity>(Func<IServiceProvider, IAsyncAddOperator<TEntity>> operatorFactory = null)
        {
            operatorFactory = GetOperatorFactory<TEntity, IAsyncAddOperator<TEntity>>(operatorFactory, _defaultAsyncAddOperatorType);

            var result = new AsyncAddOperatorConfiguration<TEntity>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncAddOperator<TEntity>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncAddOperatorConfiguration<TEntity, TInput> ConfigureAsyncAddOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncAddOperator<TEntity, TInput>> operatorFactory = null)
        {
            operatorFactory = GetOperatorFactory<TEntity, TInput, IAsyncAddOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncAddOperatorTypeWithCustomInput);

            var result = new AsyncAddOperatorConfiguration<TEntity, TInput>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncAddOperator<TEntity, TInput>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity> ConfigureAsyncUpdateOperator<TEntity>(Func<IServiceProvider, IAsyncUpdateOperator<TEntity>> operatorFactory = null)
        {
            operatorFactory = GetOperatorFactory<TEntity, IAsyncUpdateOperator<TEntity>>(operatorFactory, _defaultAsyncUpdateOperatorType);

            var result = new AsyncUpdateOperatorConfiguration<TEntity>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncUpdateOperator<TEntity>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncUpdateOperatorConfiguration<TEntity, TInput> ConfigureAsyncUpdateOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncUpdateOperator<TEntity, TInput>> operatorFactory = null)
        {
            operatorFactory = GetOperatorFactory<TEntity, TInput, IAsyncUpdateOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncUpdateOperatorTypeWithCustomInput);

            var result = new AsyncUpdateOperatorConfiguration<TEntity, TInput>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncUpdateOperator<TEntity, TInput>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity> ConfigureAsyncDeleteOperator<TEntity>(Func<IServiceProvider, IAsyncDeleteOperator<TEntity>> operatorFactory = null)
        {
            operatorFactory = GetOperatorFactory<TEntity, IAsyncDeleteOperator<TEntity>>(operatorFactory, _defaultAsyncDeleteOperatorType);

            var result = new AsyncDeleteOperatorConfiguration<TEntity>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncDeleteOperator<TEntity>), result.Build, _serviceLifetime));

            return result;
        }

        public IAsyncDeleteOperatorConfiguration<TEntity, TInput> ConfigureAsyncDeleteOperator<TEntity, TInput>(Func<IServiceProvider, IAsyncDeleteOperator<TEntity, TInput>> operatorFactory = null)
        {
            operatorFactory = GetOperatorFactory<TEntity, TInput, IAsyncDeleteOperator<TEntity, TInput>>(operatorFactory, _defaultAsyncDeleteOperatorTypeWithCustomInput);

            var result = new AsyncDeleteOperatorConfiguration<TEntity, TInput>(operatorFactory);

            _configurations.Add(result);
            _operatorServiceDescriptors.Add(new ServiceDescriptor(typeof(IAsyncDeleteOperator<TEntity, TInput>), result.Build, _serviceLifetime));

            return result;
        }

        private Func<IServiceProvider, TOperator> GetOperatorFactory<TEntity, TOperator>(Func<IServiceProvider, TOperator> operatorFactory, Type defaultType)
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

            return operatorFactory;
        }

        private Func<IServiceProvider, TOperator> GetOperatorFactory<TEntity, TInput, TOperator>(Func<IServiceProvider, TOperator> operatorFactory, Type defaultType)
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

            return operatorFactory;
        }

        public void AddOperators(IServiceCollection services)
        {
            foreach (var operatorServiceDescriptor in _operatorServiceDescriptors)
            {
                services.Add(operatorServiceDescriptor);
            }

            foreach (var crudOperatorCollectionServiceDescriptor in _crudOperatorCollectionServiceDescriptors)
            {
                services.Add(crudOperatorCollectionServiceDescriptor);
            }
        }
    }
}
