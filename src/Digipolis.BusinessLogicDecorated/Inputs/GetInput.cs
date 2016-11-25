using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Inputs
{
    public class GetInput<TEntity>
    {
        public Func<IQueryable<TEntity>, IQueryable<TEntity>> Includes { get; set; }
    }
}
