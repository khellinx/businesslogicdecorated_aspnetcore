using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic.Inputs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        // GET api/people
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IAsyncQueryOperator<Person> op)
        {
            try
            {
                var result = await op.QueryAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        // GET api/people/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromServices] IAsyncGetOperator<Person, GetPersonInput> op, int id, [FromQuery]string fields)
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

        // POST api/people
        [HttpPost]
        public void Post([FromServices] IAsyncAddOperator<Person> op, [FromBody]string value)
        {

        }

        // PUT api/people/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/people/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
