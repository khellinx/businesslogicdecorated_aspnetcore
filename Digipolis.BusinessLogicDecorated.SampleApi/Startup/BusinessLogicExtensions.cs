using Digipolis.BusinessLogicDecorated.Configuration;
using Digipolis.BusinessLogicDecorated.Configuration.Extensions;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic.Inputs;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic.Preprocessors;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Startup
{
    public static class BusinessLogicExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            var operatorBuilder = new OperatorBuilder();

            // Set the default operator implementations
            operatorBuilder.SetDefaultAsyncGetOperatorTypes(typeof(AsyncGetOperator<>), typeof(AsyncGetOperator<,>));
            operatorBuilder.SetDefaultAsyncQueryOperatorTypes(typeof(AsyncQueryOperator<>), typeof(AsyncQueryOperator<,>));
            operatorBuilder.SetDefaultAsyncAddOperatorTypes(typeof(AsyncAddOperator<>), typeof(AsyncAddOperator<,>));
            operatorBuilder.SetDefaultAsyncUpdateOperatorTypes(typeof(AsyncUpdateOperator<>), typeof(AsyncUpdateOperator<,>));
            operatorBuilder.SetDefaultAsyncDeleteOperatorTypes(typeof(AsyncDeleteOperator<>), typeof(AsyncDeleteOperator<,>));

            // Configure specific operators and their decorators
            operatorBuilder.ConfigureAsyncGetOperator<Person, GetPersonInput>()
                .WithPreprocessing<Person, GetPersonInput, PersonPreprocessor>();
            operatorBuilder.ConfigureAsyncQueryOperator<Person>()
                .WithPreprocessing<Person, PersonPreprocessor>();
            operatorBuilder.ConfigureAsyncAddOperator<Person>()
                .WithValidation<Person, PersonValidator>();
            operatorBuilder.ConfigureAsyncAddOperator<Person>()
                .WithValidation<Person, PersonValidator>();

            // Add all configured operators to the servicecollection
            operatorBuilder.AddOperators(services);

            return services;
        }
    }
}
