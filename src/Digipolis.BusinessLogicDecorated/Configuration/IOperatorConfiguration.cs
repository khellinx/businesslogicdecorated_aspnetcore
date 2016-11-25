using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IOperatorConfiguration
    {
        Type OperatorType { get; }
        Type EntityType { get; }
    }

    public interface IOperatorConfiguration<TOperator> : IOperatorConfiguration
    {
        Func<IServiceProvider, TOperator> OperatorFactory { get; }
        IList<Func<TOperator, IServiceProvider, TOperator>> Decorators { get; }

        TOperator Build(IServiceProvider serviceProvider);
    }
}
