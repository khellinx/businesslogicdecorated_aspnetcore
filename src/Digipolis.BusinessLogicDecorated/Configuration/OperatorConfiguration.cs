using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public class OperatorConfiguration<TOperator> : IOperatorConfiguration<TOperator>
    {
        public OperatorConfiguration(Func<IServiceProvider, TOperator> operatorFactory)
        {
            OperatorFactory = operatorFactory;
            Decorators = new List<Func<TOperator, IServiceProvider, TOperator>>();
        }

        public Func<IServiceProvider, TOperator> OperatorFactory { get; }
        public IList<Func<TOperator, IServiceProvider, TOperator>> Decorators { get; }

        public TOperator Build(IServiceProvider serviceProvider)
        {
            var op = OperatorFactory(serviceProvider);

            foreach (var decoratorFunc in Decorators)
            {
                op = decoratorFunc(op, serviceProvider);
            }

            return op;
        }
    }
}
