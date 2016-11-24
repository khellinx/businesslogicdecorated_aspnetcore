using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using System.Diagnostics;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic.Inputs;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery]string fields, [FromServices] IAsyncGetOperator<Person, GetPersonInput> op)
        {
            try
            {
                var result = await op.GetAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
