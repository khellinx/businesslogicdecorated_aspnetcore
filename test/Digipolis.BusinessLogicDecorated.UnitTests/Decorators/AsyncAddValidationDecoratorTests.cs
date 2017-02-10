using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.UnitTests._Entities;
using Digipolis.BusinessLogicDecorated.UnitTests._Shared;
using Digipolis.BusinessLogicDecorated.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Digipolis.BusinessLogicDecorated.UnitTests.Decorators
{
    public class AsyncAddValidationDecoratorTests
    {
        private LazyMock<IAsyncAddOperator<Person, PersonWriteInput>> _operatorLazyMock = new LazyMock<IAsyncAddOperator<Person, PersonWriteInput>>();
        private Mock<IAsyncAddOperator<Person, PersonWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncAddOperator<Person, PersonWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IAddValidator<Person, PersonWriteInput>> _validatorLazyMock = new LazyMock<IAddValidator<Person, PersonWriteInput>>();
        private Mock<IAddValidator<Person, PersonWriteInput>> ValidatorMock => _validatorLazyMock.Mock;
        private IAddValidator<Person, PersonWriteInput> Validator => _validatorLazyMock.Object;

        private LazyMock<IAsyncAddValidator<Person, PersonWriteInput>> _asyncValidatorLazyMock = new LazyMock<IAsyncAddValidator<Person, PersonWriteInput>>();
        private Mock<IAsyncAddValidator<Person, PersonWriteInput>> AsyncValidatorMock => _asyncValidatorLazyMock.Mock;
        private IAsyncAddValidator<Person, PersonWriteInput> AsyncValidator => _asyncValidatorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncAddOperator<Person, PersonWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncAddValidationDecorator<Person, PersonWriteInput>(op, Validator);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullValidator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAddValidator<Person, PersonWriteInput> validator = null;

            // Act
            Action ctorAction = () => new AsyncAddValidationDecorator<Person, PersonWriteInput>(Operator, validator);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncValidator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncAddValidator<Person, PersonWriteInput> validator = null;

            // Act
            Action ctorAction = () => new AsyncAddValidationDecorator<Person, PersonWriteInput>(Operator, validator);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncAddValidationDecorator<Person, PersonWriteInput>(Operator, Validator);

            // Assert
            Assert.Equal(Operator, decorator.AddOperator);
            Assert.Equal(Validator, decorator.Validator);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncAddValidationDecorator<Person, PersonWriteInput>(Operator, AsyncValidator);

            // Assert
            Assert.Equal(Operator, decorator.AddOperator);
            Assert.Equal(AsyncValidator, decorator.AsyncValidator);
        }

        [Fact]
        public async Task AddAsync_Should_CallValidatorValidateForAddOnce()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            ValidatorMock
                .Setup(x => x.ValidateForAdd(person, input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncAddValidationDecorator<Person, PersonWriteInput>(Operator, Validator);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "validator"));
        }

        [Fact]
        public async Task AddAsync_Should_CallAsyncValidatorValidateForAddOnce()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncValidatorMock
                .Setup(x => x.ValidateForAdd(person, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncAddValidationDecorator<Person, PersonWriteInput>(Operator, AsyncValidator);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "validator"));
        }

        [Fact]
        public async Task AddAsync_Should_CallValidatorValidateForAddFirst()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            ValidatorMock
                .Setup(x => x.ValidateForAdd(person, input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncAddValidationDecorator<Person, PersonWriteInput>(Operator, Validator);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.True(calls.First() == "validator");
        }

        [Fact]
        public async Task AddAsync_Should_CallAsyncValidatorValidateForAddFirst()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncValidatorMock
                .Setup(x => x.ValidateForAdd(person, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncAddValidationDecorator<Person, PersonWriteInput>(Operator, AsyncValidator);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.True(calls.First() == "validator");
        }

        [Fact]
        public async Task AddAsync_Should_ReturnResultOfOperator()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person));
            ValidatorMock
                .Setup(x => x.ValidateForAdd(person, input));

            var decorator = new AsyncAddValidationDecorator<Person, PersonWriteInput>(Operator, Validator);

            // Act
            var result = await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}
