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
    public class AsyncGetPreprocessingDecoratorTests
    {
        private LazyMock<IAsyncGetOperator<Person, int, PersonGetInput>> _operatorLazyMock = new LazyMock<IAsyncGetOperator<Person, int, PersonGetInput>>();
        private Mock<IAsyncGetOperator<Person, int, PersonGetInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncGetOperator<Person, int, PersonGetInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IGetPreprocessor<Person, int, PersonGetInput>> _preprocessorLazyMock = new LazyMock<IGetPreprocessor<Person, int, PersonGetInput>>();
        private Mock<IGetPreprocessor<Person, int, PersonGetInput>> PreprocessorMock => _preprocessorLazyMock.Mock;
        private IGetPreprocessor<Person, int, PersonGetInput> Preprocessor => _preprocessorLazyMock.Object;

        private LazyMock<IAsyncGetPreprocessor<Person, int, PersonGetInput>> _asyncPreprocessorLazyMock = new LazyMock<IAsyncGetPreprocessor<Person, int, PersonGetInput>>();
        private Mock<IAsyncGetPreprocessor<Person, int, PersonGetInput>> AsyncPreprocessorMock => _asyncPreprocessorLazyMock.Mock;
        private IAsyncGetPreprocessor<Person, int, PersonGetInput> AsyncPreprocessor => _asyncPreprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncGetOperator<Person, int, PersonGetInput> op = null;

            // Act
            Action ctorAction = () => new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(op, Preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IGetPreprocessor<Person, int, PersonGetInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncGetPreprocessor<Person, int, PersonGetInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(Operator, Preprocessor);

            // Assert
            Assert.Equal(Operator, decorator.GetOperator);
            Assert.Equal(Preprocessor, decorator.Preprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(Operator, AsyncPreprocessor);

            // Assert
            Assert.Equal(Operator, decorator.GetOperator);
            Assert.Equal(AsyncPreprocessor, decorator.AsyncPreprocessor);
        }

        [Fact]
        public async Task GetAsync_Should_CallPreprocessorPreprocessForGetOnce()
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
            PreprocessorMock
                .Setup(x => x.PreprocessForGet(ref id, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(Operator, Preprocessor);

            // Act
            await decorator.GetAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task GetAsync_Should_CallAsyncPreprocessorPreprocessForGetOnce()
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
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForGet(id, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.GetAsync(id, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task GetAsync_Should_CallPreprocessorPreprocessForGetFirst()
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
            PreprocessorMock
                .Setup(x => x.PreprocessForGet(ref id, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(Operator, Preprocessor);

            // Act
            await decorator.GetAsync(id, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
        }

        [Fact]
        public async Task GetAsync_Should_CallAsyncPreprocessorPreprocessForGetFirst()
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
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForGet(id, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.GetAsync(id, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
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
            PreprocessorMock
                .Setup(x => x.PreprocessForGet(ref id, ref input));

            var decorator = new AsyncGetPreprocessingDecorator<Person, int, PersonGetInput>(Operator, Preprocessor);

            // Act
            var result = await decorator.GetAsync(id, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}
