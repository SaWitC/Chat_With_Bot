using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Rabitmq;

namespace ServerApp.Controllers
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

            await _publishEndpoint.Publish<IMessage>(new MessageModel { Text=message});

            return Ok("Сообщение отправлено");
        }

        [Route("[action]/{message}")]
        [HttpGet]
        public async Task<IActionResult> test()
        {
            //_mqService.SendMessage(message);

            //await _publishEndpoint.Publish<IMessage>(new MessageModel { Text = message });

            return Ok("done");
        }
    }
}
