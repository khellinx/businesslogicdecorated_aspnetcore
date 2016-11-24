using Digipolis.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Entities
{
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Person Partner { get; set; }
    }
}
