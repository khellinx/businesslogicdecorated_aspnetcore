using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Operators
{
    public class AsyncPersonDeleteOperator : Worker, IAsyncDeleteOperator<Person, int?, object>
    {
        public AsyncPersonDeleteOperator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }

        public async Task DeleteAsync(int? id, object input = null)
        {
            if (!id.HasValue)
                return;

            var uow = UnitOfWorkScope.GetUnitOfWork(true);

            var repository = uow.GetRepository<Person>();

            repository.Remove(id.Value);
            await uow.SaveChangesAsync();
        }
    }
}
