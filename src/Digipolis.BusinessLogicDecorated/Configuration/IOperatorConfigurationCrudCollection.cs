using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IOperatorConfigurationCrudCollection<TEntity> : IOperatorConfigurationCrudCollection<TEntity, QueryInput<TEntity>, GetInput<TEntity>>
    {
    }

    public interface IOperatorConfigurationCrudCollection<TEntity, TQueryInput> : IOperatorConfigurationCrudCollection<TEntity, TQueryInput, GetInput<TEntity>>
        where TQueryInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
    {
    }

    public interface IOperatorConfigurationCrudCollection<TEntity, TQueryInput, TGetInput>
        where TGetInput : IHasIncludes<TEntity>
        where TQueryInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
    {
        IOperatorConfiguration<IAsyncGetOperator<TEntity, TGetInput>> GetOperatorConfiguration { get; }
        IOperatorConfiguration<IAsyncQueryOperator<TEntity, TQueryInput>> QueryOperatorConfiguration { get; }
        IOperatorConfiguration<IAsyncAddOperator<TEntity>> AddOperatorConfiguration { get; }
        IOperatorConfiguration<IAsyncUpdateOperator<TEntity>> UpdateOperatorConfiguration { get; }
        IOperatorConfiguration<IAsyncDeleteOperator<TEntity>> DeleteOperatorConfiguration { get; }
    }
}
