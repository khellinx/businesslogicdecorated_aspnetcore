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
        private LazyMock<IAsyncUpdateOperator<Person, PersonWriteInput>> _operatorLazyMock = new LazyMock<IAsyncUpdateOperator<Person, PersonWriteInput>>();
        private Mock<IAsyncUpdateOperator<Person, PersonWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncUpdateOperator<Person, PersonWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IUpdatePreprocessor<Person, PersonWriteInput>> _preprocessorLazyMock = new LazyMock<IUpdatePreprocessor<Person, PersonWriteInput>>();
        private Mock<IUpdatePreprocessor<Person, PersonWriteInput>> PreprocessorMock => _preprocessorLazyMock.Mock;
        private IUpdatePreprocessor<Person, PersonWriteInput> Preprocessor => _preprocessorLazyMock.Object;

        private LazyMock<IAsyncUpdatePreprocessor<Person, PersonWriteInput>> _asyncPreprocessorLazyMock = new LazyMock<IAsyncUpdatePreprocessor<Person, PersonWriteInput>>();
        private Mock<IAsyncUpdatePreprocessor<Person, PersonWriteInput>> AsyncPreprocessorMock => _asyncPreprocessorLazyMock.Mock;
        private IAsyncUpdatePreprocessor<Person, PersonWriteInput> AsyncPreprocessor => _asyncPreprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncUpdateOperator<Person, PersonWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(op, Preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IUpdatePreprocessor<Person, PersonWriteInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncUpdatePreprocessor<Person, PersonWriteInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(Operator, Preprocessor);

            // Assert
            Assert.Equal(Operator, decorator.UpdateOperator);
            Assert.Equal(Preprocessor, decorator.Preprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(Operator, AsyncPreprocessor);

            // Assert
            Assert.Equal(Operator, decorator.UpdateOperator);
            Assert.Equal(AsyncPreprocessor, decorator.AsyncPreprocessor);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallPreprocessorPreprocessForUpdateOnce()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForUpdate(ref person, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(Operator, Preprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForUpdate(person, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(Operator, AsyncPreprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForUpdate(ref person, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(Operator, Preprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForUpdate(person, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(Operator, AsyncPreprocessor);

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
            PersonWriteInput input = null;

            OperatorMock
                .Setup(x => x.UpdateAsync(person, input))
                .Returns(() => Task.FromResult(person));
            PreprocessorMock
                .Setup(x => x.PreprocessForUpdate(ref person, ref input));

            var decorator = new AsyncUpdatePreprocessingDecorator<Person, PersonWriteInput>(Operator, Preprocessor);

            // Act
            var result = await decorator.UpdateAsync(person, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}
