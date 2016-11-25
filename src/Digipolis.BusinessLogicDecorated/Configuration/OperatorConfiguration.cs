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
        public OperatorConfiguration(Type entityType, Func<IServiceProvider, TOperator> operatorFactory)
        {
            EntityType = entityType;
            OperatorType = typeof(TOperator);
            OperatorFactory = operatorFactory;
            Decorators = new List<Func<TOperator, IServiceProvider, TOperator>>();
        }

        public Type EntityType { get; }
        public Type OperatorType { get; }
        public Func<IServiceProvider, TOperator> OperatorFactory { get; protected set; }
        public IList<Func<TOperator, IServiceProvider, TOperator>> Decorators { get; }

        public virtual TOperator Build(IServiceProvider serviceProvider)
        {
            var op = OperatorFactory(serviceProvider);

            foreach (var decoratorFunc in Decorators)
            {
                op = decoratorFunc(op, serviceProvider);
            }

            return op;
        }

        public IOperatorConfiguration<TOperator> InsertDecoratorBeforeOperator(Func<TOperator, IServiceProvider, TOperator> decorator)
        {
            Decorators.Insert(0, decorator);

            return this;
        }

        public IOperatorConfiguration<TOperator> SurroundWithDecorator(Func<TOperator, IServiceProvider, TOperator> decorator)
        {
            Decorators.Add(decorator);

            return this;
        }
    }
}
