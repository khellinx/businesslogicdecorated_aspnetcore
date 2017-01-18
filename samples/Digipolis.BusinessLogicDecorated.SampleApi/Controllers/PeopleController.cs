using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Paging;
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
        public async Task<IActionResult> Get([FromServices] IAsyncQueryOperator<Person> op, [FromQuery]Page page)
        {
            try
            {
                if (page == null) page = new Page();
                if (page.Number <= 0) page.Number = 1;
                if (page.Size <= 0) page.Size = 2;

                var result = await op.QueryAsync(page);
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
        public async Task<IActionResult> Delete([FromServices] IAsyncDeleteOperator<Person, int?, object> op, int id)
        {
            await op.DeleteAsync(id);
            return NoContent();
        }
    }
}
