using Digipolis.BusinessLogicDecorated.Configuration;
using Digipolis.BusinessLogicDecorated.Inputs;
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

            // Set the default operator implementations. This should be done first.
            operatorBuilder.SetDefaultAsyncGetOperatorTypes(typeof(AsyncGetOperator<>), typeof(AsyncGetOperator<,>));
            operatorBuilder.SetDefaultAsyncQueryOperatorTypes(typeof(AsyncQueryOperator<>), typeof(AsyncQueryOperator<,>));
            operatorBuilder.SetDefaultAsyncAddOperatorTypes(typeof(AsyncAddOperator<>), typeof(AsyncAddOperator<,>));
            operatorBuilder.SetDefaultAsyncUpdateOperatorTypes(typeof(AsyncUpdateOperator<>), typeof(AsyncUpdateOperator<,>));
            operatorBuilder.SetDefaultAsyncDeleteOperatorTypes(typeof(AsyncDeleteOperator<>), typeof(AsyncDeleteOperator<,>));

            // Configure operators in one time for the Home entity
            operatorBuilder.ConfigureAsyncCrudOperators<Home, GetInput<Home>, QueryInput<Home>>()
                .WithValidation<HomeValidator>();

            // Configure operators and their decorators separately for Person entity
            operatorBuilder.ConfigureAsyncGetOperator<Person, GetPersonInput>()
                .WithPreprocessing<PersonPreprocessor>();
            operatorBuilder.ConfigureAsyncQueryOperator<Person>()
                .WithPreprocessing<PersonPreprocessor>();
            operatorBuilder.ConfigureAsyncAddOperator<Person>()
                .WithValidation<PersonValidator>();
            operatorBuilder.ConfigureAsyncUpdateOperator<Person>()
                .WithValidation<PersonValidator>();
            operatorBuilder.ConfigureAsyncDeleteOperator<Person>()
                .WithValidation<PersonValidator>();

            // Add all configured operators to the servicecollection
            operatorBuilder.AddOperators(services);

            return services;
        }
    }
}
