using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        // GET 
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Hello", "User" };
        }
        // GET api/values
        [HttpGet("{message}")]
        public ActionResult<string> Get(string message)
        {
            if (message == "hello")
                return "hi";
            else if (message == "hi")
                return "hello";
            return "Invalid token";
        }
    }
}
