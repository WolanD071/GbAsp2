using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GbWebApp.Interfaces;

namespace GbWebApp.ServiceHosting.Controllers
{
    [Route(WebApiRoutes.TestWebAPI)]
    [ApiController]
    public class ValuesApiController : ControllerBase
    {
        private static readonly List<string> __values = Enumerable
            .Range(1, 10).Select(i => $"val-{i:00}").ToList();

        [HttpGet]
        public IEnumerable<string> Get() => __values;

        [HttpGet("{id}")] // http://localhost:5000/api/values/5
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __values.Count)
                return NotFound();
            return __values[id];
        }

        [HttpPost]                 // post -> http://localhost:5000/api/values
        [HttpPost("add")]   // post -> http://localhost:5000/api/values/add
        public ActionResult Post(/*[FromBody] ??? M$ ???*/ string value)
        {
            __values.Add(value);
            return CreatedAtAction(nameof(Get), new { id = __values.Count - 1 }); // http://localhost:5000/api/values/10
        }

        [HttpPut("{id}")]       // put -> http://localhost:5000/api/values/5
        [HttpPut("edit/{id}")]  // put -> http://localhost:5000/api/values/edit/5
        public ActionResult Put(int id, string value)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __values.Count)
                return NotFound();
            __values[id] = value;
            return Ok();
        }

        [HttpDelete("{id}")] // delete -> http://localhost:5000/api/values/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __values.Count)
                return NotFound();
            __values.RemoveAt(id);
            return Ok();
        }
    }
}
