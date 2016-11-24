using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Validators;
using Digipolis.BusinessLogicDecorated.Configuration.Extensions;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public class OperatorConfigurationCrudCollection<TEntity, TQueryInput, TGetInput> : IOperatorConfigurationCrudCollection<TEntity, TQueryInput, TGetInput>
        where TQueryInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
        where TGetInput : IHasIncludes<TEntity>
    {
        public OperatorConfigurationCrudCollection(
            IOperatorConfiguration<IAsyncGetOperator<TEntity, TGetInput>> getOperatorConfiguration,
            IOperatorConfiguration<IAsyncQueryOperator<TEntity, TQueryInput>> queryOperatorConfiguration,
            IOperatorConfiguration<IAsyncAddOperator<TEntity>> addOperatorConfiguration,
            IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> updateOperatorConfiguration,
            IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> deleteOperatorConfiguration
            )
        {
            GetOperatorConfiguration = getOperatorConfiguration;
            QueryOperatorConfiguration = queryOperatorConfiguration;
            AddOperatorConfiguration = addOperatorConfiguration;
            UpdateOperatorConfiguration = updateOperatorConfiguration;
            DeleteOperatorConfiguration = deleteOperatorConfiguration;
        }

        public IOperatorConfiguration<IAsyncGetOperator<TEntity, TGetInput>> GetOperatorConfiguration { get; private set; }
        public IOperatorConfiguration<IAsyncQueryOperator<TEntity, TQueryInput>> QueryOperatorConfiguration { get; private set; }
        public IOperatorConfiguration<IAsyncAddOperator<TEntity>> AddOperatorConfiguration { get; private set; }
        public IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> UpdateOperatorConfiguration { get; private set; }
        public IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> DeleteOperatorConfiguration { get; private set; }

        public IOperatorConfigurationCrudCollection<TEntity, TQueryInput, TGetInput> WithValidation<TValidator>()
            where TValidator : class, IAddValidator<TEntity>, IUpdateValidator<TEntity>, IDeleteValidator<TEntity>
        {
            AddOperatorConfiguration.WithValidation<TEntity, TValidator>();
            UpdateOperatorConfiguration.WithValidation<TEntity, TValidator>();
            DeleteOperatorConfiguration.WithValidation<TEntity, TValidator>();

            return this;
        }
    }
}
