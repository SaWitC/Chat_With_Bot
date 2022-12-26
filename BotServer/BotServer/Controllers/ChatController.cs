using BotServer.Application.DataServices;
using BotServer.Features.Features.Commands.Chat.CreateChatCommand;
using BotServer.Features.Features.Commands.Chat.DeleteChatCommand;
using BotServer.Features.Features.Queries.Chat.GetChatById;
using BotServer.Features.Features.Queries.Chat.GetMyChats;
//using BotServer.SignalR.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.Swagger;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BotServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHttpContextService _httpContextService;
        private readonly IMediator _mediatr;

        public ChatController(IMediator mediatr, IHttpContextService httpContextService)
        {
            _mediatr = mediatr;
            _httpContextService = httpContextService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Create new chat", OperationId = "CreateChat")]
        public async Task<IActionResult> CreateChat(CreateChatCommand createChatCommand)
        {
            var res = await _mediatr.Send(createChatCommand);
            return Ok(res);
        }
        [HttpGet("getMy/{page}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "get my chates", OperationId = "GetMyChats")]
        public async Task<IActionResult> GetMyChats(int page)
        {

            var h = Request.Headers;
            GetMyChatsQuery query = new GetMyChatsQuery();
            query.AvtorId = _httpContextService.GetCurentUserId();
            query.Page = page;
            var res = await _mediatr.Send(query);
            return Ok(res);
        }
        [HttpGet("Details/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "get Details about chat", OperationId = "Details")]
        public async Task<IActionResult> Details(string id)
        {
            var query = new GetChatByIdQuery();
            query.id = id;
            var res = await _mediatr.Send(query);
            if (res != null)
                return Ok(res);
            else
                throw new Exception("it did not work; try again later");
        }

        // DELETE api/<ChatController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Delete chat", OperationId = "Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteChatCommand();
            command.id = id;
            var res = await _mediatr.Send(command);
            if (res)
                return Ok();
            else
                throw new Exception("it did not work; try again later");
        }
    }
}
