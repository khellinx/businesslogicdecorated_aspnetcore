using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration.Extensions
{
    public static class OperatorConfigurationCrudCollectionExtensions
    {
        public static IOperatorConfigurationCrudCollection<TEntity, TQueryInput, TGetInput> WithReadPreprocessing<TEntity, TQueryInput, TGetInput, TPreprocessor>(this IOperatorConfigurationCrudCollection<TEntity, TQueryInput, TGetInput> configurations)
            where TGetInput : IHasIncludes<TEntity>
            where TQueryInput : IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
            where TPreprocessor : class, IGetPreprocessor<TEntity, TGetInput>, IQueryPreprocessor<TEntity, TQueryInput>
        {
            configurations.GetOperatorConfiguration.WithPreprocessing<TEntity, TGetInput, TPreprocessor>();
            configurations.QueryOperatorConfiguration.WithPreprocessing<TEntity, TQueryInput, TPreprocessor>();

            return configurations;
        }

        public static IOperatorConfigurationCrudCollection<TEntity> WithWritePreprocessing<TEntity, TPreprocessor>(this IOperatorConfigurationCrudCollection<TEntity> configurations)
            where TPreprocessor : class, IAddPreprocessor<TEntity>, IUpdatePreprocessor<TEntity>, IDeletePreprocessor<TEntity>
        {
            configurations.AddOperatorConfiguration.WithPreprocessing<TEntity, TPreprocessor>();
            configurations.UpdateOperatorConfiguration.WithPreprocessing<TEntity, TPreprocessor>();
            configurations.DeleteOperatorConfiguration.WithPreprocessing<TEntity, TPreprocessor>();

            return configurations;
        }

        public static IOperatorConfigurationCrudCollection<TEntity> WithValidation<TEntity, TPreprocessor>(this IOperatorConfigurationCrudCollection<TEntity> configurations)
            where TPreprocessor : class, IAddPreprocessor<TEntity>, IUpdatePreprocessor<TEntity>, IDeletePreprocessor<TEntity>
        {
            configurations.AddOperatorConfiguration.WithPreprocessing<TEntity, TPreprocessor>();
            configurations.UpdateOperatorConfiguration.WithPreprocessing<TEntity, TPreprocessor>();
            configurations.DeleteOperatorConfiguration.WithPreprocessing<TEntity, TPreprocessor>();

            return configurations;
        }
    }
}
