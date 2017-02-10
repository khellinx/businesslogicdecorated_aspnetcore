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
    public class AsyncGetPostprocessingDecoratorTests
    {
        private LazyMock<IAsyncGetOperator<Person, int, PersonGetInput>> _operatorLazyMock = new LazyMock<IAsyncGetOperator<Person, int, PersonGetInput>>();
        private Mock<IAsyncGetOperator<Person, int, PersonGetInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncGetOperator<Person, int, PersonGetInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IGetPostprocessor<Person, int, PersonGetInput>> _postprocessorLazyMock = new LazyMock<IGetPostprocessor<Person, int, PersonGetInput>>();
        private Mock<IGetPostprocessor<Person, int, PersonGetInput>> PostprocessorMock => _postprocessorLazyMock.Mock;
        private IGetPostprocessor<Person, int, PersonGetInput> Postprocessor => _postprocessorLazyMock.Object;

        private LazyMock<IAsyncGetPostprocessor<Person, int, PersonGetInput>> _asyncPostprocessorLazyMock = new LazyMock<IAsyncGetPostprocessor<Person, int, PersonGetInput>>();
        private Mock<IAsyncGetPostprocessor<Person, int, PersonGetInput>> AsyncPostprocessorMock => _asyncPostprocessorLazyMock.Mock;
        private IAsyncGetPostprocessor<Person, int, PersonGetInput> AsyncPostprocessor => _asyncPostprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncGetOperator<Person, int, PersonGetInput> op = null;

            // Act
            Action ctorAction = () => new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(op, Postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IGetPostprocessor<Person, int, PersonGetInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncGetPostprocessor<Person, int, PersonGetInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(Operator, Postprocessor);

            // Assert
            Assert.Equal(Operator, decorator.GetOperator);
            Assert.Equal(Postprocessor, decorator.Postprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(Operator, AsyncPostprocessor);

            // Assert
            Assert.Equal(Operator, decorator.GetOperator);
            Assert.Equal(AsyncPostprocessor, decorator.AsyncPostprocessor);
        }

        [Fact]
        public async Task GetAsync_Should_CallPostprocessorPostprocessForGetOnce()
        {
            // Arrange
            var id = 1;
            var person = new Person();
            PersonGetInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.GetAsync(id, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForGet(id, input, ref person))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(Operator, Postprocessor);

            // Act
            await decorator.GetAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task GetAsync_Should_CallAsyncPostprocessorPostprocessForGetOnce()
        {
            // Arrange
            var id = 1;
            var person = new Person();
            PersonGetInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.GetAsync(id, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForGet(id, input, person))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.GetAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task GetAsync_Should_CallPostprocessorPostprocessForGetLast()
        {
            // Arrange
            var id = 1;
            var person = new Person();
            PersonGetInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.GetAsync(id, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForGet(id, input, ref person))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(Operator, Postprocessor);

            // Act
            await decorator.GetAsync(id, input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
        }

        [Fact]
        public async Task GetAsync_Should_CallAsyncPostprocessorPostprocessForGetLast()
        {
            // Arrange
            var id = 1;
            var person = new Person();
            PersonGetInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.GetAsync(id, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForGet(id, input, person))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.GetAsync(id, input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
        }

        [Fact]
        public async Task GetAsync_Should_ReturnResultOfOperator()
        {
            // Arrange
            var id = 1;
            var person = new Person();
            PersonGetInput input = null;

            OperatorMock
                .Setup(x => x.GetAsync(id, input))
                .Returns(() => Task.FromResult(person));
            PostprocessorMock
                .Setup(x => x.PostprocessForGet(id, input, ref person));

            var decorator = new AsyncGetPostprocessingDecorator<Person, int, PersonGetInput>(Operator, Postprocessor);

            // Act
            var result = await decorator.GetAsync(id, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}
