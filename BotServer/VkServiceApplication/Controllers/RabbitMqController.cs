using MassTransit;
using Microsoft.AspNetCore.Mvc;
using VkNet.Model;
using VkServiceApplication.Models;

namespace VkServiceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqController : ControllerBase
    {


        private readonly IPublishEndpoint _publishEndpoint;

        public RabbitMqController(IPublishEndpoint publishEndpoint)
        {
            this._publishEndpoint = publishEndpoint;
        }

        [Route("[action]/{message}")]
        [HttpGet]
        public async Task<IActionResult> SendMessage(string message)
        {
            //_mqService.SendMessage(message);

            await _publishEndpoint.Publish<MessageModel>(new MessageModel { Text = message });

            return Ok("Сообщение отправлено");
        }



        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> test()
        {
            await _publishEndpoint.Publish<MessageModel>(new MessageModel { Text = "12323" });

            return Ok("done");
        }
    }

   
}
