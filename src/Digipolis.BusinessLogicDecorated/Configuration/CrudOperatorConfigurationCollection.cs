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
            if (getTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                GetOperatorConfiguration.WithPostprocessing(serviceProvider => (IGetPostprocessor<TEntity, TGetInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
            }
            if (queryTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                QueryOperatorConfiguration.WithPostprocessing(serviceProvider => (IQueryPostprocessor<TEntity, TQueryInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
            }
            if (addTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                AddOperatorConfiguration.WithPostprocessing(serviceProvider => (IAddPostprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
            }
            if (updateTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                UpdateOperatorConfiguration.WithPostprocessing(serviceProvider => (IUpdatePostprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
            }
            if (deleteTypeInfo.IsAssignableFrom(CustomOperatorTypeInfo))
            {
                DeleteOperatorConfiguration.WithPostprocessing(serviceProvider => (IDeletePostprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, CustomOperatorType));
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
            if (getTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                GetOperatorConfiguration.WithPostprocessing(serviceProvider => (IGetPostprocessor<TEntity, TGetInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
            }
            if (queryTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                QueryOperatorConfiguration.WithPostprocessing(serviceProvider => (IQueryPostprocessor<TEntity, TQueryInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
            }
            if (addTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                AddOperatorConfiguration.WithPostprocessing(serviceProvider => (IAddPostprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
            }
            if (updateTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                UpdateOperatorConfiguration.WithPostprocessing(serviceProvider => (IUpdatePostprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
            }
            if (deleteTypeInfo.IsAssignableFrom(PostprocessorTypeInfo))
            {
                DeleteOperatorConfiguration.WithPostprocessing(serviceProvider => (IDeletePostprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, PostprocessorType));
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
            if (getTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                GetOperatorConfiguration.WithPreprocessing(serviceProvider => (IGetPreprocessor<TEntity, TGetInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
            }
            if (queryTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                QueryOperatorConfiguration.WithPreprocessing(serviceProvider => (IQueryPreprocessor<TEntity, TQueryInput>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
            }
            if (addTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                AddOperatorConfiguration.WithPreprocessing(serviceProvider => (IAddPreprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
            }
            if (updateTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                UpdateOperatorConfiguration.WithPreprocessing(serviceProvider => (IUpdatePreprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
            }
            if (deleteTypeInfo.IsAssignableFrom(preprocessorTypeInfo))
            {
                DeleteOperatorConfiguration.WithPreprocessing(serviceProvider => (IDeletePreprocessor<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, preprocessorType));
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

            // Only add validators to operators for which the given type implements the correct validator interface.
            if (addTypeInfo.IsAssignableFrom(validatorTypeInfo))
            {
                AddOperatorConfiguration.WithValidation(serviceProvider => (IAddValidator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, validatorType));
            }
            if (updateTypeInfo.IsAssignableFrom(validatorTypeInfo))
            {
                UpdateOperatorConfiguration.WithValidation(serviceProvider => (IUpdateValidator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, validatorType));
            }
            if (deleteTypeInfo.IsAssignableFrom(validatorTypeInfo))
            {
                DeleteOperatorConfiguration.WithValidation(serviceProvider => (IDeleteValidator<TEntity>)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, validatorType));
            }

            return this;
        }
    }
}
