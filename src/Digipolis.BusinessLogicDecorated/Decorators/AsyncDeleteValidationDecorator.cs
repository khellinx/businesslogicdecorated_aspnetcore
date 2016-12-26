using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncDeleteValidationDecorator<TEntity> : AsyncDeleteValidationDecorator<TEntity, object>, IAsyncDeleteOperator<TEntity>
    {
        public AsyncDeleteValidationDecorator(IAsyncDeleteOperator<TEntity, object> deleteOperator, IDeleteValidator<TEntity, object> validator) : base(deleteOperator, validator)
        {
        }

        public AsyncDeleteValidationDecorator(IAsyncDeleteOperator<TEntity, object> deleteOperator, IAsyncDeleteValidator<TEntity, object> validator) : base(deleteOperator, validator)
        {
        }
    }

    public class AsyncDeleteValidationDecorator<TEntity, TInput> : AsyncDeleteDecorator<TEntity, TInput>
    {
        public AsyncDeleteValidationDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IDeleteValidator<TEntity, TInput> validator) : base(deleteOperator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            Validator = validator;
        }

        public AsyncDeleteValidationDecorator(IAsyncDeleteOperator<TEntity, TInput> deleteOperator, IAsyncDeleteValidator<TEntity, TInput> validator) : base(deleteOperator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            AsyncValidator = validator;
        }

        public IDeleteValidator<TEntity, TInput> Validator { get; private set; }
        public IAsyncDeleteValidator<TEntity, TInput> AsyncValidator { get; private set; }

        public override async Task DeleteAsync(int id, TInput input = default(TInput))
        {
            if (Validator != null)
            {
                Validator.ValidateForDelete(id, input);
            }
            if (AsyncValidator != null)
            {
                await AsyncValidator.ValidateForDelete(id, input);
            }

            await DeleteOperator.DeleteAsync(id, input);
        }
    }
}
