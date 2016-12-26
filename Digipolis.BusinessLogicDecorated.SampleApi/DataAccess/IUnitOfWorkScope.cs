using Digipolis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.DataAccess
{
    public interface IUnitOfWorkScope : IDisposable
    {
        IUnitOfWork GetUnitOfWork(bool trackChanges = true);
    }
}
