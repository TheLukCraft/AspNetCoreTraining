using Microsoft.AspNetCore.Mvc;
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
        public CourseController()
        {
        }
        [HttpGet]
        [Route("getMessage")]
        public IActionResult GetMessage()
        {
            return Ok();
        }
    }
}
