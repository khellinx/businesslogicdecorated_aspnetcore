using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Paging
{
    public class Page
    {
        public virtual int Number { get; set; }
        public virtual int Size { get; set; }
        public virtual string[] Order { get; set; }
    }
}
