using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IOperatorConfiguration<TOperator>
    {
        Func<IServiceProvider, TOperator> OperatorFactory { get; }
        IList<Func<TOperator, IServiceProvider, TOperator>> DecoratorFactories { get; }

        TOperator Build(IServiceProvider serviceProvider);
    }
}
