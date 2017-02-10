using Digipolis.BusinessLogicDecorated.Decorators;
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
    public class AsyncDeletePostprocessingDecoratorTests
    {
        private LazyMock<IAsyncDeleteOperator<Person, int, CustomWriteInput>> _operatorLazyMock = new LazyMock<IAsyncDeleteOperator<Person, int, CustomWriteInput>>();
        private Mock<IAsyncDeleteOperator<Person, int, CustomWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncDeleteOperator<Person, int, CustomWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IDeletePostprocessor<Person, int, CustomWriteInput>> _postprocessorLazyMock = new LazyMock<IDeletePostprocessor<Person, int, CustomWriteInput>>();
        private Mock<IDeletePostprocessor<Person, int, CustomWriteInput>> PostprocessorMock => _postprocessorLazyMock.Mock;
        private IDeletePostprocessor<Person, int, CustomWriteInput> Postprocessor => _postprocessorLazyMock.Object;

        private LazyMock<IAsyncDeletePostprocessor<Person, int, CustomWriteInput>> _asyncPostprocessorLazyMock = new LazyMock<IAsyncDeletePostprocessor<Person, int, CustomWriteInput>>();
        private Mock<IAsyncDeletePostprocessor<Person, int, CustomWriteInput>> AsyncPostprocessorMock => _asyncPostprocessorLazyMock.Mock;
        private IAsyncDeletePostprocessor<Person, int, CustomWriteInput> AsyncPostprocessor => _asyncPostprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncDeleteOperator<Person, int, CustomWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(op, Postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IDeletePostprocessor<Person, int, CustomWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncDeletePostprocessor<Person, int, CustomWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(Operator, Postprocessor);

            // Assert
            Assert.Equal(Operator, decorator.DeleteOperator);
            Assert.Equal(Postprocessor, decorator.Postprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(Operator, AsyncPostprocessor);

            // Assert
            Assert.Equal(Operator, decorator.DeleteOperator);
            Assert.Equal(AsyncPostprocessor, decorator.AsyncPostprocessor);
        }

        [Fact]
        public async Task DeleteAsync_Should_CallPostprocessorPostprocessForDeleteOnce()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(Operator, Postprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task DeleteAsync_Should_CallAsyncPostprocessorPostprocessForDeleteOnce()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task DeleteAsync_Should_CallPostprocessorPostprocessForDeleteLast()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(Operator, Postprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
        }

        [Fact]
        public async Task DeleteAsync_Should_CallAsyncPostprocessorPostprocessForDeleteLast()
        {
            // Arrange
            var id = 1;
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
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
            PostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, CustomWriteInput>(Operator, Postprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "operator"));
        }
    }
}
