using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using Digipolis.BusinessLogicDecorated.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Validators
{
    public class HomeValidator : Worker, IAsyncAddValidator<Home>, IAsyncUpdateValidator<Home>
    {
        public HomeValidator(IUnitOfWorkScope uowScope) : base(uowScope)
        {
        }

        public async Task ValidateForAdd(Home entity, object input = null)
        {
            await ValidateForWrite(entity);
        }

        public async Task ValidateForUpdate(Home entity, object input = null)
        {
            await ValidateForWrite(entity);
        }

        private async Task ValidateForWrite(Home entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(entity.Address))
                throw new ArgumentNullException(nameof(entity.Address));

            var hasUniqueAddress = await HasUniqueAddress(entity);
            if (!hasUniqueAddress)
            {
                throw new ArgumentException("There is already a home with the provided address.");
            }
        }

        private async Task<bool> HasUniqueAddress(Home entity)
        {
            var uow = UnitOfWorkScope.GetUnitOfWork(false);
            var repository = uow.GetRepository<Home>();

            var exists = await repository.AnyAsync(x => x.Address.Equals(entity.Address, StringComparison.OrdinalIgnoreCase));
            return !exists;
        }
    }
}
