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

        public IDeleteValidator<TEntity, TInput> Validator { get; private set; }

        public override Task<TEntity> DeleteAsync(int id, TInput input = default(TInput))
        {
            Validator.Validate(id, input);

            return DeleteOperator.DeleteAsync(id, input);
        }
    }
}
