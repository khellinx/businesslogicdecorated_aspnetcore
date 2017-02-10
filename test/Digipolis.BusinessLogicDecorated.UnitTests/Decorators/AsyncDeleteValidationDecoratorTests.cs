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
    public class AsyncDeleteValidationDecoratorTests
    {
        private LazyMock<IAsyncDeleteOperator<Person, int, CustomWriteInput>> _operatorLazyMock = new LazyMock<IAsyncDeleteOperator<Person, int, CustomWriteInput>>();
        private Mock<IAsyncDeleteOperator<Person, int, CustomWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncDeleteOperator<Person, int, CustomWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IDeleteValidator<Person, int, CustomWriteInput>> _validatorLazyMock = new LazyMock<IDeleteValidator<Person, int, CustomWriteInput>>();
        private Mock<IDeleteValidator<Person, int, CustomWriteInput>> ValidatorMock => _validatorLazyMock.Mock;
        private IDeleteValidator<Person, int, CustomWriteInput> Validator => _validatorLazyMock.Object;

        private LazyMock<IAsyncDeleteValidator<Person, int, CustomWriteInput>> _asyncValidatorLazyMock = new LazyMock<IAsyncDeleteValidator<Person, int, CustomWriteInput>>();
        private Mock<IAsyncDeleteValidator<Person, int, CustomWriteInput>> AsyncValidatorMock => _asyncValidatorLazyMock.Mock;
        private IAsyncDeleteValidator<Person, int, CustomWriteInput> AsyncValidator => _asyncValidatorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncDeleteOperator<Person, CustomWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(op, Validator);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullValidator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IDeleteValidator<Person, CustomWriteInput> validator = null;

            // Act
            Action ctorAction = () => new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(Operator, validator);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncValidator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncDeleteValidator<Person, CustomWriteInput> validator = null;

            // Act
            Action ctorAction = () => new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(Operator, validator);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(Operator, Validator);

            // Assert
            Assert.Equal(Operator, decorator.DeleteOperator);
            Assert.Equal(Validator, decorator.Validator);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(Operator, AsyncValidator);

            // Assert
            Assert.Equal(Operator, decorator.DeleteOperator);
            Assert.Equal(AsyncValidator, decorator.AsyncValidator);
        }

        [Fact]
        public async Task DeleteAsync_Should_CallValidatorValidateForDeleteOnce()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            ValidatorMock
                .Setup(x => x.ValidateForDelete(id, input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(Operator, Validator);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "validator"));
        }

        [Fact]
        public async Task DeleteAsync_Should_CallAsyncValidatorValidateForDeleteOnce()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            AsyncValidatorMock
                .Setup(x => x.ValidateForDelete(id, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(Operator, AsyncValidator);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "validator"));
        }

        [Fact]
        public async Task DeleteAsync_Should_CallValidatorValidateForDeleteFirst()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            ValidatorMock
                .Setup(x => x.ValidateForDelete(id, input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(Operator, Validator);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.True(calls.First() == "validator");
        }

        [Fact]
        public async Task DeleteAsync_Should_CallAsyncValidatorValidateForDeleteFirst()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            AsyncValidatorMock
                .Setup(x => x.ValidateForDelete(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(Operator, AsyncValidator);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.True(calls.First() == "validator");
        }

        [Fact]
        public async Task DeleteAsync_Should_CallOperatorDeleteAsyncOnce()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            ValidatorMock
                .Setup(x => x.ValidateForDelete(id, input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncDeleteValidationDecorator<Person, int, CustomWriteInput>(Operator, Validator);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "operator"));
        }
    }
}
