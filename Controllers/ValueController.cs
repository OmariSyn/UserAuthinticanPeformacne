using BasicAuthWS.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BasicAuthWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        [BasicAuthentication] // Apply the BasicAuthentication filter to this action
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}  