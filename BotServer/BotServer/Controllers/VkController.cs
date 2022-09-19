using BotServer.Domain.Models.VkModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace BotServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VkController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IVkApi _vkApi;
        public VkController(IConfiguration configuration,IVkApi vkApi)
        {
            this._configuration = configuration;
            _vkApi = vkApi; 
        }

        [HttpPost]
        [Route("Callback")]
        public IActionResult Callback([FromBody] Updates updates)
        {
            // Проверяем, что находится в поле "type" 
            switch (updates.Type)
            {
                // Если это уведомление для подтверждения адреса
                case "confirmation":
                    // Отправляем строку для подтверждения 
                    return Ok(_configuration["Config:Confirmation"]);
                case "message_new":
                {
                    // Десериализация
                    var msg = Message.FromJson(new VkResponse(updates.Object));

                    // Отправим в ответ полученный от пользователя текст
                    _vkApi.Messages.Send(new MessagesSendParams
                    {
                        RandomId = new DateTime().Millisecond,
                        PeerId = msg.PeerId.Value,
                        Message = msg.Text
                    });
                    break;
                }
            }
            // Возвращаем "ok" серверу Callback API
            return Ok("ok");
        }
    }
}
