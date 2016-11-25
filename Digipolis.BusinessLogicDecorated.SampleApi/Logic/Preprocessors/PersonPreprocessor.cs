using Digipolis.BusinessLogicDecorated.Preprocessors;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic.Inputs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Paging;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Preprocessors
{
    public class PersonPreprocessor : IGetPreprocessor<Person, GetPersonInput>, IQueryPreprocessor<Person>
    {
        public void PreprocessForQuery(ref QueryInput<Person> input)
        {
            if (input == null) input = new QueryInput<Person>();

            input.Order = person => person.OrderByDescending(x => x.Name);
        }

        public void PreprocessForQuery(ref Page page, ref QueryInput<Person> input)
        {
            PreprocessForQuery(ref input);
        }

        public void PreprocessForGet(ref GetPersonInput input)
        {
            if (input == null) input = new GetPersonInput();

            input.Includes = person => person.Include(x => x.Partner);
        }
    }
}
