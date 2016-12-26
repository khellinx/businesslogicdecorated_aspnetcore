using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic
{
    public abstract class Worker
    {
        protected Worker(IUnitOfWorkScope uowScope)
        {
            if (uowScope == null) throw new ArgumentNullException(nameof(uowScope));

            UnitOfWorkScope = uowScope;
        }

        public IUnitOfWorkScope UnitOfWorkScope
        {
            get;
            private set;
        }
    }
}
