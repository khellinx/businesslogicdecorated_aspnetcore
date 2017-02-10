using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
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
    public class AsyncUpdatePreprocessingDecoratorTests
    {
        private LazyMock<IAsyncUpdateOperator<Person, CustomWriteInput>> _operatorLazyMock = new LazyMock<IAsyncUpdateOperator<Person, CustomWriteInput>>();
        private Mock<IAsyncUpdateOperator<Person, CustomWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncUpdateOperator<Person, CustomWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IUpdatePreprocessor<Person, CustomWriteInput>> _preprocessorLazyMock = new LazyMock<IUpdatePreprocessor<Person, CustomWriteInput>>();
        private Mock<IUpdatePreprocessor<Person, CustomWriteInput>> PreprocessorMock => _preprocessorLazyMock.Mock;
        private IUpdatePreprocessor<Person, CustomWriteInput> Preprocessor => _preprocessorLazyMock.Object;

        private LazyMock<IAsyncUpdatePreprocessor<Person, CustomWriteInput>> _asyncPreprocessorLazyMock = new LazyMock<IAsyncUpdatePreprocessor<Person, CustomWriteInput>>();
        private Mock<IAsyncUpdatePreprocessor<Person, CustomWriteInput>> AsyncPreprocessorMock => _asyncPreprocessorLazyMock.Mock;
        private IAsyncUpdatePreprocessor<Person, CustomWriteInput> AsyncPreprocessor => _asyncPreprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncUpdateOperator<Person, CustomWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(op, Preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IUpdatePreprocessor<Person, CustomWriteInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncUpdatePreprocessor<Person, CustomWriteInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(Operator, Preprocessor);

            // Assert
            Assert.Equal(Operator, decorator.UpdateOperator);
            Assert.Equal(Preprocessor, decorator.Preprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(Operator, AsyncPreprocessor);

            // Assert
            Assert.Equal(Operator, decorator.UpdateOperator);
            Assert.Equal(AsyncPreprocessor, decorator.AsyncPreprocessor);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallPreprocessorPreprocessForUpdateOnce()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForUpdate(ref person, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(Operator, Preprocessor);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task UpdateAsync_Should_CallAsyncPreprocessorPreprocessForUpdateOnce()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForUpdate(person, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task UpdateAsync_Should_CallPreprocessorPreprocessForUpdateFirst()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForUpdate(ref person, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(Operator, Preprocessor);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
        }

        [Fact]
        public async Task UpdateAsync_Should_CallAsyncPreprocessorPreprocessForUpdateFirst()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForUpdate(person, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.UpdateAsync(person, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
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
            PreprocessorMock
                .Setup(x => x.PreprocessForUpdate(ref person, ref input));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, CustomWriteInput>(Operator, Preprocessor);

            // Act
            var result = await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}
