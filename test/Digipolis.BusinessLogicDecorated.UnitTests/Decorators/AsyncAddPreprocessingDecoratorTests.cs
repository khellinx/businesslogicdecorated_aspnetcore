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
    public class AsyncAddPreprocessingDecoratorTests
    {
        private LazyMock<IAsyncAddOperator<Person, PersonWriteInput>> _operatorLazyMock = new LazyMock<IAsyncAddOperator<Person, PersonWriteInput>>();
        private Mock<IAsyncAddOperator<Person, PersonWriteInput>> OperatorMock => _operatorLazyMock.Mock;
        private IAsyncAddOperator<Person, PersonWriteInput> Operator => _operatorLazyMock.Object;

        private LazyMock<IAddPreprocessor<Person, PersonWriteInput>> _preprocessorLazyMock = new LazyMock<IAddPreprocessor<Person, PersonWriteInput>>();
        private Mock<IAddPreprocessor<Person, PersonWriteInput>> PreprocessorMock => _preprocessorLazyMock.Mock;
        private IAddPreprocessor<Person, PersonWriteInput> Preprocessor => _preprocessorLazyMock.Object;

        private LazyMock<IAsyncAddPreprocessor<Person, PersonWriteInput>> _asyncPreprocessorLazyMock = new LazyMock<IAsyncAddPreprocessor<Person, PersonWriteInput>>();
        private Mock<IAsyncAddPreprocessor<Person, PersonWriteInput>> AsyncPreprocessorMock => _asyncPreprocessorLazyMock.Mock;
        private IAsyncAddPreprocessor<Person, PersonWriteInput> AsyncPreprocessor => _asyncPreprocessorLazyMock.Object;

        [Fact]
        public void ConstructorWithNullOperator_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncAddOperator<Person, PersonWriteInput> op = null;

            // Act
            Action ctorAction = () => new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(op, Preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAddPreprocessor<Person, PersonWriteInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithNullAsyncPreprocessor_Should_ThrowArgumentNullException()
        {
            // Arrange
            IAsyncAddPreprocessor<Person, PersonWriteInput> preprocessor = null;

            // Act
            Action ctorAction = () => new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(Operator, preprocessor);

            // Assert
            Assert.Throws<ArgumentNullException>(ctorAction);
        }

        [Fact]
        public void ConstructorWithValidParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(Operator, Preprocessor);

            // Assert
            Assert.Equal(Operator, decorator.AddOperator);
            Assert.Equal(Preprocessor, decorator.Preprocessor);
        }

        [Fact]
        public void ConstructorWithValidAsyncParams_Should_SetProperties()
        {
            // Arrange

            // Act
            var decorator = new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(Operator, AsyncPreprocessor);

            // Assert
            Assert.Equal(Operator, decorator.AddOperator);
            Assert.Equal(AsyncPreprocessor, decorator.AsyncPreprocessor);
        }

        [Fact]
        public async Task AddAsync_Should_CallPreprocessorPreprocessForAddOnce()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForAdd(ref person, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(Operator, Preprocessor);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task AddAsync_Should_CallAsyncPreprocessorPreprocessForAddOnce()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForAdd(person, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(1, calls.Count(x => x == "preprocessor"));
        }

        [Fact]
        public async Task AddAsync_Should_CallPreprocessorPreprocessForAddFirst()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            PreprocessorMock
                .Setup(x => x.PreprocessForAdd(ref person, ref input))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(Operator, Preprocessor);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
        }

        [Fact]
        public async Task AddAsync_Should_CallAsyncPreprocessorPreprocessForAddFirst()
        {
            // Arrange
            var person = new Person();
            PersonWriteInput input = null;
            var calls = new List<string>();

            OperatorMock
                .Setup(x => x.AddAsync(person, input))
                .Returns(() => Task.FromResult(person))
                .Callback(() => calls.Add("operator"));
            AsyncPreprocessorMock
                .Setup(x => x.PreprocessForAdd(person, input))
                .Returns(Task.FromResult<object>(null))
                .Callback(() => calls.Add("preprocessor"));

            var decorator = new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(Operator, AsyncPreprocessor);

            // Act
            await decorator.AddAsync(person, input);

            // Assert
            Assert.True(calls.First() == "preprocessor");
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
            PreprocessorMock
                .Setup(x => x.PreprocessForAdd(ref person, ref input));

            var decorator = new AsyncAddPreprocessingDecorator<Person, PersonWriteInput>(Operator, Preprocessor);

            // Act
            var result = await decorator.AddAsync(person, input);

            // Assert
            Assert.Equal(person, result);
        }
    }
}
