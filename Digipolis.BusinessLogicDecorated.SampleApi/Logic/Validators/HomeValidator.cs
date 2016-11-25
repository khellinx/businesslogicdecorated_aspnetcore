using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using Digipolis.BusinessLogicDecorated.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Validators
{
    public class HomeValidator : IAddValidator<Home>, IUpdateValidator<Home>
    {
        public void ValidateForAdd(Home entity, object input = null)
        {
            ValidateForWrite(entity);
        }

        public void ValidateForUpdate(Home entity, object input = null)
        {
            ValidateForWrite(entity);
        }

        private void ValidateForWrite(Home entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(entity.Address))
                throw new ArgumentNullException(nameof(entity.Address));
        }
    }
}
