using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Repositories
{
    // This class does not satisfy modern coding best-practices (e.g.: it is not thread safe).
    // It just serves a simple purpose for these examples.
    // In real-world scenario's, the repository would probably connect to a database of some sort.

    public class PersonRepository : IRepository<Person>
    {
        private static IList<Person> _data;

        public PersonRepository()
        {
            if (_data == null)
            {
                var john = new Person()
                {
                    Name = "John",
                    Birthdate = new DateTime(1986, 5, 21)
                };
                var maria = new Person()
                {
                    Name = "Maria",
                    Birthdate = new DateTime(1983, 7, 14)
                };
                // John and Maria are a couple, how cute
                john.Partner = maria;
                maria.Partner = john;

                var peter = new Person()
                {
                    Name = "Peter",
                    Birthdate = new DateTime(1953, 1, 9)
                };

                _data = new List<Person>();
                Add(john);
                Add(maria);
                Add(peter);
            }
        }

        public Person Get(int id)
        {
            
        }

        public IEnumerable<Person> Query(Func<IQueryable<Person>, IQueryable<Person>> includes, Func<IQueryable<Person>, IQueryable<Person>> filter, Func<IQueryable<Person>, IOrderedQueryable<Person>> order)
        {
            throw new NotImplementedException();
        }

        public Person Add(Person entity)
        {
            throw new NotImplementedException();
        }

        public Person Update(Person entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
