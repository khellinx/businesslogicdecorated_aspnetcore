using Digipolis.BusinessLogicDecorated.Inputs;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Controllers
{
    [Route("api/[controller]")]
    public class HomesController : Controller
    {
        public HomesController(ICrudOperatorCollection<Home> operatorCollection)
        {
            OperatorCollection = operatorCollection;
        }

        public ICrudOperatorCollection<Home> OperatorCollection { get; private set; }

        // GET api/homes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await OperatorCollection.QueryAsync();
            return Ok(result);
        }

        // GET api/homes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery]string fields)
        {
            var result = await OperatorCollection.GetAsync(id);
            return Ok(result);
        }

        // POST api/homes
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/homes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/homes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
