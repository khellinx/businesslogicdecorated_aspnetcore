using Digipolis.BusinessLogicDecorated.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Configuration
{
    public interface IAsyncDeleteOperatorConfiguration<TEntity, TInput> : IOperatorConfiguration<IAsyncDeleteOperator<TEntity, TInput>>
    {
    }
}
