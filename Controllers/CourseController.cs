using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Controllers
{
    [ApiController]
    [Route("Course")]
    public class CourseController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CourseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("getMessage")]
        public IActionResult GetMessage()
        {
            var refreshTime = _configuration.GetValue<int>("Application:RefreshTime");
            var message = new Message()
            {
                Content = $"My refresh time is: {refreshTime}",
                Author = "Łukasz"
            };
            return Ok(message);
        }
        [HttpPost]
        [Route("sendMessage")]
        public IActionResult SendMessage([FromBody]Message message)
        {
            return Ok(message);
        }
    }
}
