using Digipolis.BusinessLogicDecorated.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput>
        where TGetInput : GetInput<TEntity>
        where TQueryInput : QueryInput<TEntity>
    {
        IAsyncGetOperatorConfiguration<TEntity, TGetInput> GetOperatorConfiguration { get; }
        IAsyncQueryOperatorConfiguration<TEntity, TQueryInput> QueryOperatorConfiguration { get; }
        IAsyncAddOperatorConfiguration<TEntity> AddOperatorConfiguration { get; }
        IAsyncUpdateOperatorConfiguration<TEntity> UpdateOperatorConfiguration { get; }
        IAsyncDeleteOperatorConfiguration<TEntity> DeleteOperatorConfiguration { get; }

        ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput> WithPostprocessing<TPostprocessor>()
            where TPostprocessor : class;

        ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput> WithPreprocessing<TPreprocessor>()
            where TPreprocessor : class;

        ICrudOperatorConfigurationCollection<TEntity, TGetInput, TQueryInput> WithValidation<TValidator>()
            where TValidator : class;
    }
}
