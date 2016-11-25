using Digipolis.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Entities
{
    public class Home : EntityBase
    {
        public string Address { get; set; }
        public int NumberOfRooms { get; set; }
    }
}
