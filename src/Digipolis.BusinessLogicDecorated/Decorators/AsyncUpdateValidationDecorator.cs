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

        public AsyncUpdateValidationDecorator(IAsyncUpdateOperator<TEntity, object> updateOperator, IAsyncUpdateValidator<TEntity, object> validator) : base(updateOperator, validator)
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

        public AsyncUpdateValidationDecorator(IAsyncUpdateOperator<TEntity, TInput> updateOperator, IAsyncUpdateValidator<TEntity, TInput> validator) : base(updateOperator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            AsyncValidator = validator;
        }

        public IUpdateValidator<TEntity, TInput> Validator { get; private set; }
        public IAsyncUpdateValidator<TEntity, TInput> AsyncValidator { get; private set; }

        public override async Task<TEntity> UpdateAsync(TEntity entity, TInput input = default(TInput))
        {
            if (Validator != null)
            {
                Validator.ValidateForUpdate(entity, input);
            }
            if (AsyncValidator != null)
            {
                await AsyncValidator.ValidateForUpdate(entity, input);
            }

            return await UpdateOperator.UpdateAsync(entity, input);
        }
    }
}
