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
    public class AsyncQueryPostprocessingDecoratorTests
    {
        private LazyMock<IAsyncQueryOperator<Person, PersonQueryInput>> _operatorLazyMock = new LazyMock<IAsyncQueryOperator<Person, PersonQueryInput>>();
        private Mock<IAsyncQueryOperator<Person, PersonQueryInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncQueryOperator<Person, PersonQueryInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IQueryPostprocessor<Person, PersonQueryInput>> _postprocessorLazyMock = new LazyMock<IQueryPostprocessor<Person, PersonQueryInput>>();
        private Mock<IQueryPostprocessor<Person, PersonQueryInput>> PostprocessorMock => _postprocessorLazyMock.Mock;
        private IQueryPostprocessor<Person, PersonQueryInput> Postprocessor => _postprocessorLazyMock.Object;

        private LazyMock<IAsyncQueryPostprocessor<Person, PersonQueryInput>> _asyncPostprocessorLazyMock = new LazyMock<IAsyncQueryPostprocessor<Person, PersonQueryInput>>();
        private Mock<IAsyncQueryPostprocessor<Person, PersonQueryInput>> AsyncPostprocessorMock => _asyncPostprocessorLazyMock.Mock;
        private IAsyncQueryPostprocessor<Person, PersonQueryInput> AsyncPostprocessor => _asyncPostprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncQueryOperator<Person, PersonQueryInput> op = null;

            // Act
            Action ctorAction = () => new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(op, Postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IQueryPostprocessor<Person, PersonQueryInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPostprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncQueryPostprocessor<Person, PersonQueryInput> postprocessor = null;

            // Act
            Action ctorAction = () => new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(Operator, postprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(Operator, Postprocessor);

            // Assert
            Assert.Equal(Operator, decorator.QueryOperator);
            Assert.Equal(Postprocessor, decorator.Postprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(Operator, AsyncPostprocessor);

            // Assert
            Assert.Equal(Operator, decorator.QueryOperator);
            Assert.Equal(AsyncPostprocessor, decorator.AsyncPostprocessor);
        }

        [Fact]
        public async Task QueryAsync_Should_CallPostprocessorPostprocessForQueryOnce()
        {
            // Arrange
            IEnumerable<Person> people = new List<Person>();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForQuery(input, ref people))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(Operator, Postprocessor);

            // Act
            await decorator.QueryAsync(input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task QueryAsync_Should_CallAsyncPostprocessorPostprocessForQueryOnce()
        {
            // Arrange
            IEnumerable<Person> people = new List<Person>();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForQuery(input, people))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.QueryAsync(input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "postprocessor"));
        }

        [Fact]
        public async Task QueryAsync_Should_CallPostprocessorPostprocessForQueryLast()
        {
            // Arrange
            IEnumerable<Person> people = new List<Person>();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            PostprocessorMock
                .Setup(x => x.PostprocessForQuery(input, ref people))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(Operator, Postprocessor);

            // Act
            await decorator.QueryAsync(input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
        }

        [Fact]
        public async Task QueryAsync_Should_CallAsyncPostprocessorPostprocessForQueryLast()
        {
            // Arrange
            IEnumerable<Person> people = new List<Person>();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            AsyncPostprocessorMock
                .Setup(x => x.PostprocessForQuery(input, people))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("postprocessor"));

            var decorator = new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(Operator, AsyncPostprocessor);

            // Act
            await decorator.QueryAsync(input);

            // Assert
            Assert.True(calls.Last() == "postprocessor");
        }

        [Fact]
        public async Task QueryAsync_Should_ReturnResultOfOperator()
        {
            // Arrange
            IEnumerable<Person> people = new List<Person>();
            PersonQueryInput input = null;

            OperatorMock
                .Setup(x => x.QueryAsync(input))
                .Returns(() => Task.FromResult(people));
            PostprocessorMock
                .Setup(x => x.PostprocessForQuery(input, ref people));

            var decorator = new AsyncQueryPostprocessingDecorator<Person, PersonQueryInput>(Operator, Postprocessor);

            // Act
            var result = await decorator.QueryAsync(input);

            // Assert
            Assert.Equal(people, result);
        }
    }
}
