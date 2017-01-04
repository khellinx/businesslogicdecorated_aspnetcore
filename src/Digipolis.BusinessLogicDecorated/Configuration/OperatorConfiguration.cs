using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public class OperatorConfiguration<TOperator> : IOperatorConfiguration<TOperator>
        where TOperator : class
    {
        public OperatorConfiguration(Func<IServiceProvider, TOperator> operatorFactory)
        {
            OperatorFactory = operatorFactory;
            DecoratorFactories = new List<Func<TOperator, IServiceProvider, TOperator>>();
        }

        public Func<IServiceProvider, TOperator> OperatorFactory { get; protected set; }
        public IList<Func<TOperator, IServiceProvider, TOperator>> DecoratorFactories { get; }

        public virtual TOperator Build(IServiceProvider serviceProvider)
        {
            var op = OperatorFactory(serviceProvider);

            foreach (var decoratorFunc in DecoratorFactories)
            {
                op = decoratorFunc(op, serviceProvider);
            }

            return op;
        }

        internal IOperatorConfiguration<TOperator> InsertDecorator(Func<TOperator, IServiceProvider, TOperator> decoratorFactory)
        {
            DecoratorFactories.Insert(0, decoratorFactory);

            return this;
        }

        internal IOperatorConfiguration<TOperator> InsertDecorator<TDependency>(Func<TOperator, TDependency, TOperator> decoratorFactory, Func<IServiceProvider, TDependency> dependencyFactory = null)
        {
            if (dependencyFactory == null)
            {
                dependencyFactory = serviceProvider => ActivatorUtilities.GetServiceOrCreateInstance<TDependency>(serviceProvider);
            }

            DecoratorFactories.Insert(0, (op, serviceProvider) => decoratorFactory(op, dependencyFactory(serviceProvider)));

            return this;
        }

        internal IOperatorConfiguration<TOperator> AppendDecorator(Func<TOperator, IServiceProvider, TOperator> decoratorFactory)
        {
            DecoratorFactories.Add(decoratorFactory);

            return this;
        }

        internal IOperatorConfiguration<TOperator> AppendDecorator<TDependency>(Func<TOperator, TDependency, TOperator> decoratorFactory, Func<IServiceProvider, TDependency> dependencyFactory = null)
        {
            if (dependencyFactory == null)
            {
                dependencyFactory = serviceProvider => ActivatorUtilities.GetServiceOrCreateInstance<TDependency>(serviceProvider); ;
            }

            DecoratorFactories.Add((op, serviceProvider) => decoratorFactory(op, dependencyFactory(serviceProvider)));

            return this;
        }
    }
}
