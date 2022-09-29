using BotServer.Domain.Models;
using BotServer.Features.Features.Commands.Vk.VkAuthorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.Swagger;
using System.Security.Claims;
//using VkNet;
//using VkNet.Abstractions;
//using VkNet.Model;
//using VkNet.Model.RequestParams;
//using VkNet.Utils;

namespace BotServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VkController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<BotServer.Domain.Models.User> _usermanager;
        //private readonly IVkApi _vkApi;
        private readonly IMediator _mediator;
        public Guid UserId => Guid.Parse(HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        public VkController(IConfiguration configuration,
           // IVkApi vkApi,
           UserManager<BotServer.Domain.Models.User> userManager,
            IMediator mediator)
        {
            this._configuration = configuration;
            //_vkApi = vkApi; 
            _mediator = mediator;
            _usermanager = userManager;

        }

        //[HttpPost("test")]
        ////[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<IActionResult> test()
        //{
        //    //var user = await _usermanager.FindByIdAsync(UserId.ToString());
        //    //VkApi vkApi = new VkApi();
        //    //vkApi.Authorize(new ApiAuthParams()
        //    //{
        //    //    AccessToken = _configuration["VkConfig:Token"]
        //    //    //ApplicationId = ulong.Parse(_configuration["VkConfig:AppId"])
        //    //});
        //    //await vkApi.Messages.SendAsync(new MessagesSendParams()
        //    //{
        //    //    UserId = 404055010,
        //    //    Message = "You loged in",
        //    //    RandomId = Environment.TickCount
        //    //});



        //    var resp = gggg();
        //    var Us = typeof(User);

        //    //var user = ;
        //    return Ok(new { user,resp });
        //}

        private object gggg()
        {
            return new User();
        }

        //[HttpPost]
        //[Route("Callback")]
        //public IActionResult Callback([FromBody] Updates updates)
        //{
        //    // Проверяем, что находится в поле "type" 
        //    //switch (updates.Type)
        //    //{
        //    //    // Если это уведомление для подтверждения адреса
        //    //    case "confirmation":
        //    //        // Отправляем строку для подтверждения 
        //    //        return Ok(_configuration["Config:Confirmation"]);
        //    //    case "message_new":
        //    //    {
        //    //        // Десериализация
        //    //        var msg = Message.FromJson(new VkResponse(updates.Object));

        //    //        // Отправим в ответ полученный от пользователя текст
        //    //        _vkApi.Messages.Send(new MessagesSendParams
        //    //        {
        //    //            RandomId = new DateTime().Millisecond,
        //    //            PeerId = msg.PeerId.Value,
        //    //            Message = msg.Text
        //    //        });
        //    //        break;
        //    //    }
        //    //}
        //    // Возвращаем "ok" серверу Callback API
        //    //return Ok(_configuration["VkConfig:Token"]);
        //    return Ok();
        //}
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "SaveVkId", OperationId = "Try conect to Vk")]
        public async Task<IActionResult> ConectToVk(string Email, string Password)
        {
            VkAIdSaveCommand vkAIdSaveCommand = new VkAIdSaveCommand();
            vkAIdSaveCommand.Email = Email;
            vkAIdSaveCommand.Password = Password;
            vkAIdSaveCommand.UserId = UserId.ToString();

            var res = await _mediator.Send(vkAIdSaveCommand);
            return Ok(res);
        }

    }
}
