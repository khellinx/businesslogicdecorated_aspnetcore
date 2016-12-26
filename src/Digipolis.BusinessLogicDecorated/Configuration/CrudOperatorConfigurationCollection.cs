using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Digipolis.BusinessLogicDecorated.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public class CrudOperatorConfigurationCollection<TEntity> : CrudOperatorConfigurationCollection<TEntity, GetInput<TEntity>, QueryInput<TEntity>>, ICrudOperatorConfigurationCollection<TEntity>
    {
        public CrudOperatorConfigurationCollection(IAsyncGetOperatorConfiguration<TEntity, GetInput<TEntity>> getOperatorConfiguration, IAsyncQueryOperatorConfiguration<TEntity, QueryInput<TEntity>> queryOperatorConfiguration, IAsyncAddOperatorConfiguration<TEntity> addOperatorConfiguration, IAsyncUpdateOperatorConfiguration<TEntity> updateOperatorConfiguration, IAsyncDeleteOperatorConfiguration<TEntity> deleteOperatorConfiguration) : base(getOperatorConfiguration, queryOperatorConfiguration, addOperatorConfiguration, updateOperatorConfiguration, deleteOperatorConfiguration)
        {
        }

        public virtual ICrudOperatorCollection<TEntity> BuildSimple(IServiceProvider serviceProvider)
        {
            var result = new CrudOperatorCollection<TEntity>(
                GetOperatorConfiguration?.Build(serviceProvider),
                QueryOperatorConfiguration?.Build(serviceProvider),
                AddOperatorConfiguration?.Build(serviceProvider),
                UpdateOperatorConfiguration?.Build(serviceProvider),
                DeleteOperatorConfiguration?.Build(serviceProvider)
                );

            return result;
        }
    }

    public class CrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput> : ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput>
        where TQueryInput : QueryInput<TEntity>
        where TGetInput : GetInput<TEntity>
    {
        public CrudOperatorConfigurationCollection(
            IAsyncGetOperatorConfiguration<TEntity, TGetInput> getOperatorConfiguration,
            IAsyncQueryOperatorConfiguration<TEntity, TQueryInput> queryOperatorConfiguration,
            IAsyncAddOperatorConfiguration<TEntity> addOperatorConfiguration,
            IAsyncUpdateOperatorConfiguration<TEntity> updateOperatorConfiguration,
            IAsyncDeleteOperatorConfiguration<TEntity> deleteOperatorConfiguration
            )
        {
            GetOperatorConfiguration = getOperatorConfiguration;
            QueryOperatorConfiguration = queryOperatorConfiguration;
            AddOperatorConfiguration = addOperatorConfiguration;
            UpdateOperatorConfiguration = updateOperatorConfiguration;
            DeleteOperatorConfiguration = deleteOperatorConfiguration;
        }

        public IAsyncGetOperatorConfiguration<TEntity, TGetInput> GetOperatorConfiguration { get; private set; }
        public IAsyncQueryOperatorConfiguration<TEntity, TQueryInput> QueryOperatorConfiguration { get; private set; }
        public IAsyncAddOperatorConfiguration<TEntity> AddOperatorConfiguration { get; private set; }
        public IAsyncUpdateOperatorConfiguration<TEntity> UpdateOperatorConfiguration { get; private set; }
        public IAsyncDeleteOperatorConfiguration<TEntity> DeleteOperatorConfiguration { get; private set; }

        public virtual ICrudOperatorCollection<TEntity, TGetInput, TQueryInput> Build(IServiceProvider serviceProvider)
        {
            var result = new CrudOperatorCollection<TEntity, TGetInput, TQueryInput>(
                GetOperatorConfiguration?.Build(serviceProvider),
                QueryOperatorConfiguration?.Build(serviceProvider),
                AddOperatorConfiguration?.Build(serviceProvider),
                UpdateOperatorConfiguration?.Build(serviceProvider),
                DeleteOperatorConfiguration?.Build(serviceProvider)
                );

            return result;
        }

        public ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput> WithCustomOperator<TCustomOperator>()
        {
            // Get the type and type info from the provided Postprocessor
            var CustomOperatorType = typeof(TCustomOperator);
            var CustomOperatorTypeInfo = CustomOperatorType.GetTypeInfo();

            // Get the type info from all Postprocessor interfaces
            var getTypeInfo = typeof(IAsyncGetOperator<TEntity, TGetInput>).GetTypeInfo();
            var queryTypeInfo = typeof(IAsyncQueryOperator<TEntity, TQueryInput>).GetTypeInfo();
            var addTypeInfo = typeof(IAsyncAddOperator<TEntity>).GetTypeInfo();
            var updateTypeInfo = typeof(IAsyncUpdateOperator<TEntity>).GetTypeInfo();
            var deleteTypeInfo = typeof(IAsyncDeleteOperator<TEntity>).GetTypeInfo();

            // Only use custom operator for which the given type implements the correct Operator interfaces.
            bool foundInterface = false;
            if (getTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                GetOperatorConfiguration.WithCustomOperator(serviceProvider => (IAsyncGetOperator<TEntity, TGetInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
                foundInterface = true;
            }
            if (queryTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                QueryOperatorConfiguration.WithCustomOperator(serviceProvider => (IAsyncQueryOperator<TEntity, TQueryInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
                foundInterface = true;
            }
            if (addTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                AddOperatorConfiguration.WithCustomOperator(serviceProvider => (IAsyncAddOperator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
                foundInterface = true;
            }
            if (updateTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                UpdateOperatorConfiguration.WithCustomOperator(serviceProvider => (IAsyncUpdateOperator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
                foundInterface = true;
            }
            if (deleteTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                DeleteOperatorConfiguration.WithCustomOperator(serviceProvider => (IAsyncDeleteOperator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
                foundInterface = true;
            }

            // If the postprocessor does not implement one of the postprocessor interfaces, throw an exception.
            if (!foundInterface)
            {
                throw new Exception($"The provided custom operator '{typeof(TCustomOperator).Name}' should at least implement one of the operator interfaces.");
            }

            return this;
        }

        public ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class
        {
            // Get the type and type info from the provided Postprocessor
            var PostprocessorType = typeof(TPostprocessor);
            var PostprocessorTypeInfo = PostprocessorType.GetTypeInfo();

            // Get the type info from all Postprocessor interfaces
            var getTypeInfo = typeof(IGetPostprocessor<TEntity, TGetInput>).GetTypeInfo();
            var queryTypeInfo = typeof(IQueryPostprocessor<TEntity, TQueryInput>).GetTypeInfo();
            var addTypeInfo = typeof(IAddPostprocessor<TEntity>).GetTypeInfo();
            var updateTypeInfo = typeof(IUpdatePostprocessor<TEntity>).GetTypeInfo();
            var deleteTypeInfo = typeof(IDeletePostprocessor<TEntity>).GetTypeInfo();

            // Only add Postprocessors to operators for which the given type implements the correct Postprocessor interface.
            bool foundInterface = false;
            if (getTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                GetOperatorConfiguration.WithPostprocessing(serviceProvider => (IGetPostprocessor<TEntity, TGetInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
                foundInterface = true;
            }
            if (queryTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                QueryOperatorConfiguration.WithPostprocessing(serviceProvider => (IQueryPostprocessor<TEntity, TQueryInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
                foundInterface = true;
            }
            if (addTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                AddOperatorConfiguration.WithPostprocessing(serviceProvider => (IAddPostprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
                foundInterface = true;
            }
            if (updateTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                UpdateOperatorConfiguration.WithPostprocessing(serviceProvider => (IUpdatePostprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
                foundInterface = true;
            }
            if (deleteTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                DeleteOperatorConfiguration.WithPostprocessing(serviceProvider => (IDeletePostprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
                foundInterface = true;
            }

            // If the postprocessor does not implement one of the postprocessor interfaces, throw an exception.
            if (!foundInterface)
            {
                throw new Exception($"The provided postprocessor '{typeof(TPostprocessor).Name}' should at least implement one of the postprocessor interfaces.");
            }

            return this;
        }

        public ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class
        {
            // Get the type and type info from the provided preprocessor
            var preprocessorType = typeof(TPreprocessor);
            var preprocessorTypeInfo = preprocessorType.GetTypeInfo();

            // Get the type info from all preprocessor interfaces
            var getTypeInfo = typeof(IGetPreprocessor<TEntity, TGetInput>).GetTypeInfo();
            var queryTypeInfo = typeof(IQueryPreprocessor<TEntity, TQueryInput>).GetTypeInfo();
            var addTypeInfo = typeof(IAddPreprocessor<TEntity>).GetTypeInfo();
            var updateTypeInfo = typeof(IUpdatePreprocessor<TEntity>).GetTypeInfo();
            var deleteTypeInfo = typeof(IDeletePreprocessor<TEntity>).GetTypeInfo();

            // Only add preprocessors to operators for which the given type implements the correct preprocessor interface.
            bool foundInterface = false;
            if (getTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                GetOperatorConfiguration.WithPreprocessing(serviceProvider => (IGetPreprocessor<TEntity, TGetInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
                foundInterface = true;
            }
            if (queryTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                QueryOperatorConfiguration.WithPreprocessing(serviceProvider => (IQueryPreprocessor<TEntity, TQueryInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
                foundInterface = true;
            }
            if (addTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                AddOperatorConfiguration.WithPreprocessing(serviceProvider => (IAddPreprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
                foundInterface = true;
            }
            if (updateTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                UpdateOperatorConfiguration.WithPreprocessing(serviceProvider => (IUpdatePreprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
                foundInterface = true;
            }
            if (deleteTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                DeleteOperatorConfiguration.WithPreprocessing(serviceProvider => (IDeletePreprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
                foundInterface = true;
            }

            // If the preprocessor does not implement one of the preprocessor interfaces, throw an exception.
            if (!foundInterface)
            {
                throw new Exception($"The provided preprocessor '{typeof(TPreprocessor).Name}' should at least implement one of preprocessor interfaces.");
            }

            return this;
        }

        public ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput> WithValidation<TValidator>()
            where TValidator : class
        {
            // Get the type and type info from the provided validator
            var validatorType = typeof(TValidator);
            var validatorTypeInfo = validatorType.GetTypeInfo();

            // Get the type info from all validator interfaces
            var addTypeInfo = typeof(IAddValidator<TEntity>).GetTypeInfo();
            var updateTypeInfo = typeof(IUpdateValidator<TEntity>).GetTypeInfo();
            var deleteTypeInfo = typeof(IDeleteValidator<TEntity>).GetTypeInfo();
            var asyncAddTypeInfo = typeof(IAsyncAddValidator<TEntity>).GetTypeInfo();
            var asyncUpdateTypeInfo = typeof(IAsyncUpdateValidator<TEntity>).GetTypeInfo();
            var asyncDeleteTypeInfo = typeof(IAsyncDeleteValidator<TEntity>).GetTypeInfo();

            // Only add validators to operators for which the given type implements the correct validator interface.
            bool foundInterface = false;
            if (addTypeInfo.IsAssignableFrom(validatorTypeInfo))
            {
                AddOperatorConfiguration.WithValidation(serviceProvider => (IAddValidator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, validatorType));
                foundInterface = true;
            }
            if (updateTypeInfo.IsAssignableFrom(validatorTypeInfo))
            {
                UpdateOperatorConfiguration.WithValidation(serviceProvider => (IUpdateValidator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, validatorType));
                foundInterface = true;
            }
            if (deleteTypeInfo.IsAssignableFrom(validatorTypeInfo))
            {
                DeleteOperatorConfiguration.WithValidation(serviceProvider => (IDeleteValidator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, validatorType));
                foundInterface = true;
            }
            if (asyncAddTypeInfo.IsAssignableFrom(validatorTypeInfo))
            {
                AddOperatorConfiguration.WithAsyncValidation(serviceProvider => (IAsyncAddValidator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, validatorType));
                foundInterface = true;
            }
            if (asyncUpdateTypeInfo.IsAssignableFrom(validatorTypeInfo))
            {
                UpdateOperatorConfiguration.WithAsyncValidation(serviceProvider => (IAsyncUpdateValidator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, validatorType));
                foundInterface = true;
            }
            if (asyncDeleteTypeInfo.IsAssignableFrom(validatorTypeInfo))
            {
                DeleteOperatorConfiguration.WithAsyncValidation(serviceProvider => (IAsyncDeleteValidator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, validatorType));
                foundInterface = true;
            }

            // If the validator does not implement one of the validator interfaces, throw an exception.
            if (!foundInterface)
            {
                throw new Exception($"The provided validator '{typeof(TValidator).Name}' should at least implement one of the validator interfaces.");
            }

            return this;
        }
    }
}
