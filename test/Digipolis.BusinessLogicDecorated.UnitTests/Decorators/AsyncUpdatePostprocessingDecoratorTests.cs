﻿using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Postprocessors;
using Digipolis.BusinessLogicDecorated.UnitTests._Entities;
using Digipolis.BusinessLogicDecorated.UnitTests._Shared;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Digipolis.BusinessLogicDecorated.UnitTests.Decorators
{
    public class AsyncUpdatePostprocessingDecoratorTests
    {
        private LazyMock<IAsyncUpdateOperator<Person, CustomWriteInput>> _operatorLazyMock = new LazyMock<IAsyncUpdateOperator<Person, CustomWriteInput>>();
        private Mock<IAsyncUpdateOperator<Person, CustomWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncUpdateOperator<Person, CustomWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IUpdatePostprocessor<Person, CustomWriteInput>> _postprocessorLazyMock = new LazyMock<IUpdatePostprocessor<Person, CustomWriteInput>>();
        private Mock<IUpdatePostprocessor<Person, CustomWriteInput>> PostprocessorMock => _postprocessorLazyMock.Mock;
        private IUpdatePostprocessor<Person, CustomWriteInput> Postprocessor => _postprocessorLazyMock.Object;

        private LazyMock<IAsyncUpdatePostprocessor<Person, CustomWriteInput>> _asyncPostprocessorLazyMock = new LazyMock<IAsyncUpdatePostprocessor<Person, CustomWriteInput>>();
        private Mock<IAsyncUpdatePostprocessor<Person, CustomWriteInput>> AsyncPostprocessorMock => _asyncPostprocessorLazyMock.Mock;
        private IAsyncUpdatePostprocessor<Person, CustomWriteInput> AsyncPostprocessor => _asyncPostprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncUpdateOperator<Person, CustomWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(op, Postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IUpdatePostprocessor<Person, CustomWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncUpdatePostprocessor<Person, CustomWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(Operator, Postprocessor);

            // Assert
            Assert.Equal(Operator, decorator.UpdateOperator);
            Assert.Equal(Postprocessor, decorator.Postprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(Operator, AsyncPostprocessor);

            // Assert
            Assert.Equal(Operator, decorator.UpdateOperator);
            Assert.Equal(AsyncPostprocessor, decorator.AsyncPostprocessor);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallPostprocessorPostprocessForUpdateOnce()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForUpdate(person, input, ref person))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(Operator, Postprocessor);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task UpdateAsync_Should_CallAsyncPostprocessorPostprocessForUpdateOnce()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForUpdate(person, input, person))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task UpdateAsync_Should_CallPostprocessorPostprocessForUpdateLast()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForUpdate(person, input, ref person))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(Operator, Postprocessor);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
        }

        [Fact]
        public async Task UpdateAsync_Should_CallAsyncPostprocessorPostprocessForUpdateLast()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForUpdate(person, input, person))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
        }

        [Fact]
        public async Task UpdateAsync_Should_ReturnResultOfOperator()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person));
            PostprocessorMock
                .Setup(x => x.PostprocessForUpdate(person, input, ref person));

            var decorator = new AsyncUpdatePostprocessingDecorator<Person, CustomWriteInput>(Operator, Postprocessor);

            // Act
            var result = await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}