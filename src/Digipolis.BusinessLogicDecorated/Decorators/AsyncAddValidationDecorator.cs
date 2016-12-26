using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Validators;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncAddValidationDecorator<TEntity> : AsyncAddValidationDecorator<TEntity, object>, IAsyncAddOperator<TEntity>
    {
        public AsyncAddValidationDecorator(IAsyncAddOperator<TEntity, object> addOperator, IAddValidator<TEntity, object> validator) : base(addOperator, validator)
        {
        }

        public AsyncAddValidationDecorator(IAsyncAddOperator<TEntity, object> addOperator, IAsyncAddValidator<TEntity, object> validator) : base(addOperator, validator)
        {
        }
    }

    public class AsyncAddValidationDecorator<TEntity, TInput> : AsyncAddDecorator<TEntity, TInput>
    {
        public AsyncAddValidationDecorator(IAsyncAddOperator<TEntity, TInput> addOperator, IAddValidator<TEntity, TInput> validator) : base(addOperator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            Validator = validator;
        }

        public AsyncAddValidationDecorator(IAsyncAddOperator<TEntity, TInput> addOperator, IAsyncAddValidator<TEntity, TInput> validator) : base(addOperator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            AsyncValidator = validator;
        }

        public IAddValidator<TEntity, TInput> Validator { get; private set; }
        public IAsyncAddValidator<TEntity, TInput> AsyncValidator { get; private set; }

        public override async Task<TEntity> AddAsync(TEntity entity, TInput input = default(TInput))
        {
            if (Validator != null)
            {
                Validator.ValidateForAdd(entity, input);
            }
            if (AsyncValidator != null)
            {
                await AsyncValidator.ValidateForAdd(entity, input);
            }

            return await AddOperator.AddAsync(entity, input);
        }
    }
}
