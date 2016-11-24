using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using Digipolis.BusinessLogicDecorated.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Logic.Validators
{
    public class PersonValidator : IAddValidator<Person>, IUpdateValidator<Person>, IDeleteValidator<Person>
    {
        public void ValidateForAdd(Person entity, object input = null)
        {
            ValidateForWrite(entity);
        }

        public void ValidateForUpdate(Person entity, object input = null)
        {
            ValidateForWrite(entity);
        }

        private void ValidateForWrite(Person entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(entity.Name))
                throw new ArgumentNullException(nameof(entity.Name));
        }

        public void ValidateForDelete(int id, object input = null)
        {
        }
    }
}
