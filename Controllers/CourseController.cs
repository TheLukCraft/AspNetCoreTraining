using AspNetCoreTraining.Database.Entities;
using AspNetCoreTraining.Database.Repositories;
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
        private readonly IMessagesRepository _messagesRepository;
        public CourseController(IConfiguration configuration, IMessagesRepository messagesRepository)
        {
            _configuration = configuration;
            _messagesRepository = messagesRepository;
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
            var messageEntity = new MessageEntity
            {
                Content = message.Content
            };
            var result =_messagesRepository.Add(messageEntity);
            if(result)
            {
                return Ok(message);
            }
            return NotFound();
        }
    }
}
