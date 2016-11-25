using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Paging
{
    public class PagedCollection<T>
    {
        public Page Page { get; set; }
        public long TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
