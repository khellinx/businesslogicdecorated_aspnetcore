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
    public class AsyncAddPostprocessingDecoratorTests
    {
        private LazyMock<IAsyncAddOperator<Person, CustomWriteInput>> _operatorLazyMock = new LazyMock<IAsyncAddOperator<Person, CustomWriteInput>>();
        private Mock<IAsyncAddOperator<Person, CustomWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncAddOperator<Person, CustomWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IAddPostprocessor<Person, CustomWriteInput>> _postprocessorLazyMock = new LazyMock<IAddPostprocessor<Person, CustomWriteInput>>();
        private Mock<IAddPostprocessor<Person, CustomWriteInput>> PostprocessorMock => _postprocessorLazyMock.Mock;
        private IAddPostprocessor<Person, CustomWriteInput> Postprocessor => _postprocessorLazyMock.Object;

        private LazyMock<IAsyncAddPostprocessor<Person, CustomWriteInput>> _asyncPostprocessorLazyMock = new LazyMock<IAsyncAddPostprocessor<Person, CustomWriteInput>>();
        private Mock<IAsyncAddPostprocessor<Person, CustomWriteInput>> AsyncPostprocessorMock => _asyncPostprocessorLazyMock.Mock;
        private IAsyncAddPostprocessor<Person, CustomWriteInput> AsyncPostprocessor => _asyncPostprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncAddOperator<Person, CustomWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(op, Postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAddPostprocessor<Person, CustomWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncAddPostprocessor<Person, CustomWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(Operator, Postprocessor);

            // Assert
            Assert.Equal(Operator, decorator.AddOperator);
            Assert.Equal(Postprocessor, decorator.Postprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(Operator, AsyncPostprocessor);

            // Assert
            Assert.Equal(Operator, decorator.AddOperator);
            Assert.Equal(AsyncPostprocessor, decorator.AsyncPostprocessor);
        }

        [Fact]
        public async Task AddAsync_Should_CallPostprocessorPostprocessForAddOnce()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, ref person))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(Operator, Postprocessor);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task AddAsync_Should_CallAsyncPostprocessorPostprocessForAddOnce()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, person))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task AddAsync_Should_CallPostprocessorPostprocessForAddLast()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, ref person))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(Operator, Postprocessor);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
        }

        [Fact]
        public async Task AddAsync_Should_CallAsyncPostprocessorPostprocessForAddLast()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, person))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
        }

        [Fact]
        public async Task AddAsync_Should_ReturnResultOfOperator()
        {
            // Arrange
            var person = new Person();
            CustomWriteInput input = null;

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person));
            PostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, ref person));

            var decorator = new AsyncAddPostprocessingDecorator<Person, CustomWriteInput>(Operator, Postprocessor);

            // Act
            var result = await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}
