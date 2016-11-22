using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Validators;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncAddValidationDecorator<TEntity, TInput> : AsyncAddDecorator<TEntity, TInput>
        where TInput : class
    {
        public AsyncAddValidationDecorator(IAsyncAddOperator<TEntity, TInput> addOperator, IAddValidator<TEntity, TInput> validator) : base(addOperator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            Validator = validator;
        }

        public IAddValidator<TEntity, TInput> Validator { get; private set; }

        public override Task<TEntity> AddAsync(TEntity entity, TInput input = null)
        {
            Validator.Validate(entity, input);

            return AddOperator.AddAsync(entity, input);
        }
    }
}
