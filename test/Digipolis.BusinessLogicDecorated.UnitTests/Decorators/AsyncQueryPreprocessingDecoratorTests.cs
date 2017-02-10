using Digipolis.BusinessLogicDecorated.Decorators;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Paging;
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
    public class AsyncQueryPreprocessingDecoratorTests
    {
        private LazyMock<IAsyncQueryOperator<Person, PersonQueryInput>> _operatorLazyMock = new LazyMock<IAsyncQueryOperator<Person, PersonQueryInput>>();
        private Mock<IAsyncQueryOperator<Person, PersonQueryInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncQueryOperator<Person, PersonQueryInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IQueryPreprocessor<Person, PersonQueryInput>> _preprocessorLazyMock = new LazyMock<IQueryPreprocessor<Person, PersonQueryInput>>();
        private Mock<IQueryPreprocessor<Person, PersonQueryInput>> PreprocessorMock => _preprocessorLazyMock.Mock;
        private IQueryPreprocessor<Person, PersonQueryInput> Preprocessor => _preprocessorLazyMock.Object;

        private LazyMock<IAsyncQueryPreprocessor<Person, PersonQueryInput>> _asyncPreprocessorLazyMock = new LazyMock<IAsyncQueryPreprocessor<Person, PersonQueryInput>>();
        private Mock<IAsyncQueryPreprocessor<Person, PersonQueryInput>> AsyncPreprocessorMock => _asyncPreprocessorLazyMock.Mock;
        private IAsyncQueryPreprocessor<Person, PersonQueryInput> AsyncPreprocessor => _asyncPreprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncQueryOperator<Person, PersonQueryInput> op = null;

            // Act
            Action ctorAction = () => new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(op, Preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IQueryPreprocessor<Person, PersonQueryInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncQueryPreprocessor<Person, PersonQueryInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, Preprocessor);

            // Assert
            Assert.Equal(Operator, decorator.QueryOperator);
            Assert.Equal(Preprocessor, decorator.Preprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, AsyncPreprocessor);

            // Assert
            Assert.Equal(Operator, decorator.QueryOperator);
            Assert.Equal(AsyncPreprocessor, decorator.AsyncPreprocessor);
        }

        [Fact]
        public async Task QueryAsync_Should_CallPreprocessorPreprocessForQueryOnce()
        {
            // Arrange
            IEnumerable<Person> people = new List<Person>();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForQuery(ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, Preprocessor);

            // Act
            await decorator.QueryAsync(input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task QueryPagedAsync_Should_CallPreprocessorPreprocessForQueryOnce()
        {
            // Arrange
            var people = new PagedCollection<Person>();
            var page = new Page();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(page, input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForQuery(ref page, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, Preprocessor);

            // Act
            await decorator.QueryAsync(page, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task QueryAsync_Should_CallAsyncPreprocessorPreprocessForQueryOnce()
        {
            // Arrange
            IEnumerable<Person> people = new List<Person>();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForQuery(input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.QueryAsync(input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task QueryPagedAsync_Should_CallAsyncPreprocessorPreprocessForQueryOnce()
        {
            // Arrange
            var people = new PagedCollection<Person>();
            var page = new Page();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(page, input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForQuery(page, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.QueryAsync(page, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task QueryAsync_Should_CallPreprocessorPreprocessForQueryFirst()
        {
            // Arrange
            IEnumerable<Person> people = new List<Person>();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForQuery(ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, Preprocessor);

            // Act
            await decorator.QueryAsync(input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
        }

        [Fact]
        public async Task QueryPagedAsync_Should_CallPreprocessorPreprocessForQueryFirst()
        {
            // Arrange
            var people = new PagedCollection<Person>();
            var page = new Page();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(page, input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForQuery(ref page, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, Preprocessor);

            // Act
            await decorator.QueryAsync(page, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
        }

        [Fact]
        public async Task QueryAsync_Should_CallAsyncPreprocessorPreprocessForQueryFirst()
        {
            // Arrange
            IEnumerable<Person> people = new List<Person>();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForQuery(input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.QueryAsync(input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
        }

        [Fact]
        public async Task QueryPagedAsync_Should_CallAsyncPreprocessorPreprocessForQueryFirst()
        {
            // Arrange
            var people = new PagedCollection<Person>();
            var page = new Page();
            PersonQueryInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.QueryAsync(page, input))
                .Returns(() => Task.FromResult(people))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForQuery(page, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.QueryAsync(page, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
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
            PreprocessorMock
                .Setup(x => x.PreprocessForQuery(ref input));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, Preprocessor);

            // Act
            var result = await decorator.QueryAsync(input);

            // Assert
            Assert.Equal(people, result);
        }

        [Fact]
        public async Task QueryPagedAsync_Should_ReturnResultOfOperator()
        {
            // Arrange
            var people = new PagedCollection<Person>();
            var page = new Page();
            PersonQueryInput input = null;

            OperatorMock
                .Setup(x => x.QueryAsync(page, input))
                .Returns(() => Task.FromResult(people));
            PreprocessorMock
                .Setup(x => x.PreprocessForQuery(ref page, ref input));

            var decorator = new AsyncQueryPreprocessingDecorator<Person, PersonQueryInput>(Operator, Preprocessor);

            // Act
            var result = await decorator.QueryAsync(page, input);

            // Assert
            Assert.Equal(people, result);
        }
    }
}
