﻿using Digipolis.BusinessLogicDecorated.Decorators;
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
    public class AsyncUpdateValidationDecoratorTests
    {
        private LazyMock<IAsyncUpdateOperator<Person, PersonWriteInput>> _operatorLazyMock = new LazyMock<IAsyncUpdateOperator<Person, PersonWriteInput>>();
        private Mock<IAsyncUpdateOperator<Person, PersonWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncUpdateOperator<Person, PersonWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IUpdateValidator<Person, PersonWriteInput>> _validatorLazyMock = new LazyMock<IUpdateValidator<Person, PersonWriteInput>>();
        private Mock<IUpdateValidator<Person, PersonWriteInput>> ValidatorMock => _validatorLazyMock.Mock;
        private IUpdateValidator<Person, PersonWriteInput> Validator => _validatorLazyMock.Object;

        private LazyMock<IAsyncUpdateValidator<Person, PersonWriteInput>> _asyncValidatorLazyMock = new LazyMock<IAsyncUpdateValidator<Person, PersonWriteInput>>();
        private Mock<IAsyncUpdateValidator<Person, PersonWriteInput>> AsyncValidatorMock => _asyncValidatorLazyMock.Mock;
        private IAsyncUpdateValidator<Person, PersonWriteInput> AsyncValidator => _asyncValidatorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncUpdateOperator<Person, PersonWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(op, Validator);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullValidator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IUpdateValidator<Person, PersonWriteInput> validator = null;

            // Act
            Action ctorAction = () => new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(Operator, validator);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncValidator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncUpdateValidator<Person, PersonWriteInput> validator = null;

            // Act
            Action ctorAction = () => new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(Operator, validator);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(Operator, Validator);

            // Assert
            Assert.Equal(Operator, decorator.UpdateOperator);
            Assert.Equal(Validator, decorator.Validator);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(Operator, AsyncValidator);

            // Assert
            Assert.Equal(Operator, decorator.UpdateOperator);
            Assert.Equal(AsyncValidator, decorator.AsyncValidator);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallValidatorValidateForUpdateOnce()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            ValidatorMock
                .Setup(x => x.ValidateForUpdate(person, input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(Operator, Validator);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "validator"));
        }

        [Fact]
        public async Task UpdateAsync_Should_CallAsyncValidatorValidateForUpdateOnce()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncValidatorMock
                .Setup(x => x.ValidateForUpdate(person, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(Operator, AsyncValidator);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "validator"));
        }

        [Fact]
        public async Task UpdateAsync_Should_CallValidatorValidateForUpdateFirst()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            ValidatorMock
                .Setup(x => x.ValidateForUpdate(person, input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(Operator, Validator);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.True(calls.First() == "validator");
        }

        [Fact]
        public async Task UpdateAsync_Should_CallAsyncValidatorValidateForUpdateFirst()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncValidatorMock
                .Setup(x => x.ValidateForUpdate(person, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(Operator, AsyncValidator);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.True(calls.First() == "validator");
        }

        [Fact]
        public async Task UpdateAsync_Should_ReturnResultOfOperator()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person));
            ValidatorMock
                .Setup(x => x.ValidateForUpdate(person, input));

            var decorator = new AsyncUpdateValidationDecorator<Person, PersonWriteInput>(Operator, Validator);

            // Act
            var result = await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}
