using Digipolis.BusinessLogicDecorated.Postprocessors;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;
using Digipolis.BusinessLogicDecorated.Paging;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Postprocessors
{
    public class HomePostprocessor : Worker, IAsyncGetPostprocessor<Home>, IQueryPostprocessor<Home>
    {
        public HomePostprocessor(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }

        public async Task PostprocessForGet(GetInput<Home> input, Home result)
        {
            if (string.IsNullOrWhiteSpace(result?.Address))
                return;

            // Get the people living on the same address.
            // Normally this would be done with an include of some kind.
            // We just included this for the sake of the async example.
            var uow = UnitOfWorkScope.GetUnitOfWork(false);
            var repository = uow.GetRepository<Person>();

            var peopleOnSameAddress = await repository.QueryAsync(x => x.Address == result.Address);
            result.Habitants = peopleOnSameAddress;
        }

        public void PostprocessForQuery(QueryInput<Home> input, ref IEnumerable<Home> result)
        {
            if (result == null)
                return;

            foreach (var home in result)
            {
                home.Address = home.Address + ", België";
            }
        }

        public void PostprocessForQuery(Page page, QueryInput<Home> input, ref PagedCollection<Home> result)
        {
            if (result?.Data == null)
                return;

            foreach (var home in result.Data)
            {
                home.Address = home.Address + ", België";
            }
        }
    }
}
