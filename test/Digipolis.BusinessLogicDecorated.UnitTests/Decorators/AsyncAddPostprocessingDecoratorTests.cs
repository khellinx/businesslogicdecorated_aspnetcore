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
        private LazyMock<IAsyncAddOperator<Person, PersonWriteInput>> _operatorLazyMock = new LazyMock<IAsyncAddOperator<Person, PersonWriteInput>>();
        private Mock<IAsyncAddOperator<Person, PersonWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncAddOperator<Person, PersonWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IAddPostprocessor<Person, PersonWriteInput>> _postprocessorLazyMock = new LazyMock<IAddPostprocessor<Person, PersonWriteInput>>();
        private Mock<IAddPostprocessor<Person, PersonWriteInput>> PostprocessorMock => _postprocessorLazyMock.Mock;
        private IAddPostprocessor<Person, PersonWriteInput> Postprocessor => _postprocessorLazyMock.Object;

        private LazyMock<IAsyncAddPostprocessor<Person, PersonWriteInput>> _asyncPostprocessorLazyMock = new LazyMock<IAsyncAddPostprocessor<Person, PersonWriteInput>>();
        private Mock<IAsyncAddPostprocessor<Person, PersonWriteInput>> AsyncPostprocessorMock => _asyncPostprocessorLazyMock.Mock;
        private IAsyncAddPostprocessor<Person, PersonWriteInput> AsyncPostprocessor => _asyncPostprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncAddOperator<Person, PersonWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(op, Postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAddPostprocessor<Person, PersonWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncAddPostprocessor<Person, PersonWriteInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(Operator, Postprocessor);

            // Assert
            Assert.Equal(Operator, decorator.AddOperator);
            Assert.Equal(Postprocessor, decorator.Postprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(Operator, AsyncPostprocessor);

            // Assert
            Assert.Equal(Operator, decorator.AddOperator);
            Assert.Equal(AsyncPostprocessor, decorator.AsyncPostprocessor);
        }

        [Fact]
        public async Task AddAsync_Should_CallPostprocessorPostprocessForAddOnce()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, ref person))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(Operator, Postprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, person))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(Operator, AsyncPostprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, ref person))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(Operator, Postprocessor);

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
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, person))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(Operator, AsyncPostprocessor);

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
            PersonWriteInput input = null;

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person));
            PostprocessorMock
                .Setup(x => x.PostprocessForAdd(person, input, ref person));

            var decorator = new AsyncAddPostprocessingDecorator<Person, PersonWriteInput>(Operator, Postprocessor);

            // Act
            var result = await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}
