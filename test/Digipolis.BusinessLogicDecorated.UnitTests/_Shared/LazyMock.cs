using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.UnitTests._Shared
{
    public class LazyMock<T>
        where T : class
    {
        private Mock<T> _mock;
        public Mock<T> Mock
        {
            get { return _mock = _mock ?? new Mock<T>(MockBehavior.Strict); }
        }

        private T _object;
        public T Object
        {
            get { return _object = _object ?? Mock.Object; }
        }
    }
}
