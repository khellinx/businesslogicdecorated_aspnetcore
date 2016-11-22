using Digipolis.BusinessLogicDecorated.Preprocessors;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Preprocessors
{
    public class PersonPreprocessor : IGetPreprocessor<Person, GetPersonInput>
    {
        public void Preprocess(GetPersonInput input)
        {
        }
    }
}
