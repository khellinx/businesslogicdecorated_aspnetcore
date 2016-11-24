using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IOperatorConfiguration
    {
        Type OperatorType { get; }
        object Build(IServiceProvider serviceProvider);
    }

    public interface IOperatorConfiguration<TOperator> : IOperatorConfiguration
    {
        Func<IServiceProvider, TOperator> OperatorFactory { get; }
        IList<Func<TOperator, IServiceProvider, TOperator>> Decorators { get; }
    }
}
