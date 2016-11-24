using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IAsyncAddOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncAddOperator<TEntity, TInput>>
    {
    }
}
