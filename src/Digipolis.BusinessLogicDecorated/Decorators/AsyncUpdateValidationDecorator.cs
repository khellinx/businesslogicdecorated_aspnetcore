using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncUpdateValidationDecorator<TEntity> : AsyncUpdateValidationDecorator<TEntity, object>, IAsyncUpdateOperator<TEntity>
    {
        public AsyncUpdateValidationDecorator(IAsyncUpdateOperator<TEntity, object> updateOperator, IUpdateValidator<TEntity, object> validator) : base(updateOperator, validator)
        {
        }
    }

    public class AsyncUpdateValidationDecorator<TEntity, TInput> : AsyncUpdateDecorator<TEntity, TInput>
    {
        public AsyncUpdateValidationDecorator(IAsyncUpdateOperator<TEntity, TInput> updateOperator, IUpdateValidator<TEntity, TInput> validator) : base(updateOperator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            Validator = validator;
        }

        public IUpdateValidator<TEntity, TInput> Validator { get; private set; }

        public override Task<TEntity> UpdateAsync(TEntity entity, TInput input = default(TInput))
        {
            Validator.ValidateForUpdate(entity, input);

            return UpdateOperator.UpdateAsync(entity, input);
        }
    }
}
