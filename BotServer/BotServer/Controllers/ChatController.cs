using BotServer.Features.Features.Commands.Chat.CreateChatCommand;
using BotServer.Features.Features.Commands.Chat.DeleteChatCommand;
using BotServer.Features.Features.Queries.Chat.GetChatById;
using BotServer.Features.Features.Queries.Chat.GetMyChats;
//using BotServer.SignalR.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        public Guid Id => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        private readonly IMediator _mediatr;
        //private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IMediator mediatr /*, IHubContext<ChatHub> hub*/)
        {
            _mediatr = mediatr;
          //  _hubContext = hub;
        }

        // POST api/<ChatController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Create new chat", OperationId = "CreateChat")]
        public async Task<IActionResult> CreateChat(CreateChatCommand createChatCommand)
        {
            
            var res =await _mediatr.Send(createChatCommand);
            return Ok(res);
        }
        [HttpGet("getMy/{page}")]
        [Authorize (AuthenticationSchemes ="Bearer")]
        [SwaggerResponse(StatusCodes.Status200OK,Type =typeof(Response))]
        [SwaggerOperation(summary:"get my chates",OperationId = "GetMyChats")]
        public async Task<IActionResult> GetMyChats(int page)
        {
            GetMyChatsQuery query = new GetMyChatsQuery();
            query.AvtorId = Id.ToString();
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
            //getChatById.id
            var res =await _mediatr.Send(query);
            return Ok(res);
        }

        // DELETE api/<ChatController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Delete chat", OperationId = "Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteChatCommand();
            command.id = id;
            var res = await _mediatr.Send(command);

            return Ok(res);
        }
    }
}
