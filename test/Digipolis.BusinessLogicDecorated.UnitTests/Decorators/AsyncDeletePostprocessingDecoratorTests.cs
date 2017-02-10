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
        private LazyMock<IAsyncDeleteOperator<Person, int, PersonWriteInput>> _operatorLazyMock = new LazyMock<IAsyncDeleteOperator<Person, int, PersonWriteInput>>();
        private Mock<IAsyncDeleteOperator<Person, int, PersonWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncDeleteOperator<Person, int, PersonWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IDeletePostprocessor<Person, int, PersonWriteInput>> _postprocessorLazyMock = new LazyMock<IDeletePostprocessor<Person, int, PersonWriteInput>>();
        private Mock<IDeletePostprocessor<Person, int, PersonWriteInput>> PostprocessorMock => _postprocessorLazyMock.Mock;
        private IDeletePostprocessor<Person, int, PersonWriteInput> Postprocessor => _postprocessorLazyMock.Object;

        private LazyMock<IAsyncDeletePostprocessor<Person, int, PersonWriteInput>> _asyncPostprocessorLazyMock = new LazyMock<IAsyncDeletePostprocessor<Person, int, PersonWriteInput>>();
        private Mock<IAsyncDeletePostprocessor<Person, int, PersonWriteInput>> AsyncPostprocessorMock => _asyncPostprocessorLazyMock.Mock;
        private IAsyncDeletePostprocessor<Person, int, PersonWriteInput> AsyncPostprocessor => _asyncPostprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncDeleteOperator<Person, int, PersonWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(op, Postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IDeletePostprocessor<Person, int, PersonWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncDeletePostprocessor<Person, int, PersonWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(Operator, Postprocessor);

            // Assert
            Assert.Equal(Operator, decorator.DeleteOperator);
            Assert.Equal(Postprocessor, decorator.Postprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(Operator, AsyncPostprocessor);

            // Assert
            Assert.Equal(Operator, decorator.DeleteOperator);
            Assert.Equal(AsyncPostprocessor, decorator.AsyncPostprocessor);
        }

        [Fact]
        public async Task DeleteAsync_Should_CallPostprocessorPostprocessForDeleteOnce()
        {
            // Arrange
            var id = 1;
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(Operator, Postprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(Operator, AsyncPostprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(Operator, Postprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(Operator, AsyncPostprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.DeleteAsync(id, input))
                .Returns(() => Task.FromResult<object>(null))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForDelete(id, input))
                .Callback(() => calls.Add("validator"));

            var decorator = new AsyncDeletePostprocessingDecorator<Person, int, PersonWriteInput>(Operator, Postprocessor);

            // Act
            await decorator.DeleteAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "operator"));
        }
    }
}
