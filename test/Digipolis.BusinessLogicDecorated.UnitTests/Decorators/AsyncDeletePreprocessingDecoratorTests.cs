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
    public class AsyncDeletePreprocessingDecoratorTests
    {
        private LazyMock<IAsyncDeleteOperator<Person, int, CustomWriteInput>> _operatorLazyMock = new LazyMock<IAsyncDeleteOperator<Person, int, CustomWriteInput>>();
        private Mock<IAsyncDeleteOperator<Person, int, CustomWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncDeleteOperator<Person, int, CustomWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IDeletePreprocessor<Person, int, CustomWriteInput>> _preprocessorLazyMock = new LazyMock<IDeletePreprocessor<Person, int, CustomWriteInput>>();
        private Mock<IDeletePreprocessor<Person, int, CustomWriteInput>> PreprocessorMock => _preprocessorLazyMock.Mock;
        private IDeletePreprocessor<Person, int, CustomWriteInput> Preprocessor => _preprocessorLazyMock.Object;

        private LazyMock<IAsyncDeletePreprocessor<Person, int, CustomWriteInput>> _asyncPreprocessorLazyMock = new LazyMock<IAsyncDeletePreprocessor<Person, int, CustomWriteInput>>();
        private Mock<IAsyncDeletePreprocessor<Person, int, CustomWriteInput>> AsyncPreprocessorMock => _asyncPreprocessorLazyMock.Mock;
        private IAsyncDeletePreprocessor<Person, int, CustomWriteInput> AsyncPreprocessor => _asyncPreprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncDeleteOperator<Person, int, CustomWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(op, Preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IDeletePreprocessor<Person, int, CustomWriteInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncDeletePreprocessor<Person, int, CustomWriteInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(Operator, Preprocessor);

            // Assert
            Assert.Equal(Operator, decorator.DeleteOperator);
            Assert.Equal(Preprocessor, decorator.Preprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(Operator, AsyncPreprocessor);

            // Assert
            Assert.Equal(Operator, decorator.DeleteOperator);
            Assert.Equal(AsyncPreprocessor, decorator.AsyncPreprocessor);
        }

        [Fact]
        public async Task DeleteAsync_Should_CallPreprocessorPreprocessForDeleteOnce()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForDelete(ref id, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(Operator, Preprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task DeleteAsync_Should_CallAsyncPreprocessorPreprocessForDeleteOnce()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForDelete(id, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task DeleteAsync_Should_CallPreprocessorPreprocessForDeleteFirst()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForDelete(ref id, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(Operator, Preprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
        }

        [Fact]
        public async Task DeleteAsync_Should_CallAsyncPreprocessorPreprocessForDeleteFirst()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForDelete(id, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
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
            PreprocessorMock
                .Setup(x => x.PreprocessForDelete(ref id, ref input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncDeletePreprocessingDecorator<Person, int, CustomWriteInput>(Operator, Preprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "operator"));
        }
    }
}
