using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Inputs
{
    public class GetPersonInput : GetInput<Person>
    {
        public string Fields { get; set; }
    }
}
