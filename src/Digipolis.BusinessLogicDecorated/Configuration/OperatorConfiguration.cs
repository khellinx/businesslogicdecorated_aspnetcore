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
            OperatorType = typeof(TOperator);
            OperatorFactory = operatorFactory;
            Decorators = new List<Func<TOperator, IServiceProvider, TOperator>>();
        }

        public Type OperatorType { get; }
        public Func<IServiceProvider, TOperator> OperatorFactory { get; }
        public IList<Func<TOperator, IServiceProvider, TOperator>> Decorators { get; }

        public object Build(IServiceProvider serviceProvider)
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
