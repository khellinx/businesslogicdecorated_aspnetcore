using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Digipolis.BusinessLogicDecorated.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration.Extensions
{
    public static class OperatorConfigurationExtensions
    {
        public static IOperatorConfiguration<TOperator> InsertDecoratorBeforeOperator<TOperator>(this IOperatorConfiguration<TOperator> configuration, Func<TOperator, IServiceProvider, TOperator> decorator)
            where TOperator : class
        {
            configuration.Decorators.Insert(0, decorator);

            return configuration;
        }

        public static IOperatorConfiguration<TOperator> SurroundWithDecorator<TOperator>(this IOperatorConfiguration<TOperator> configuration, Func<TOperator, IServiceProvider, TOperator> decorator)
            where TOperator : class
        {
            configuration.Decorators.Add(decorator);

            return configuration;
        }
    }
}
