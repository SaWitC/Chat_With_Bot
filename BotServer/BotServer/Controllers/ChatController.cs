using BotServer.Features.Features.Commands.Chat.CreateChatCommand;
using BotServer.Features.Features.Commands.Chat.DeleteChatCommand;
using BotServer.Features.Features.Queries.Chat.GetChatById;
using BotServer.Features.Features.Queries.Chat.GetMyChats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ChatController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        // POST api/<ChatController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateChat(CreateChatCommand createChatCommand)
        {
            createChatCommand.createChatDTO.avtorId = Id.ToString();
            var res =await _mediatr.Send(createChatCommand);
            return Ok(res);
        }
        [HttpGet("getMy/{page}")]
        [Authorize]
        public async Task<IActionResult> GetMyChats(int page)
        {
            GetMyChatsQuery query = new GetMyChatsQuery();
            query.AvtorId = Id.ToString();
            query.Page = page;
            var res = await _mediatr.Send(query);
            return Ok(res);
        }
        [HttpGet("Details/{page}&{id}")]
        [Authorize]
        public async Task<IActionResult> Details(int page,string id)
        {
            var query = new GetChatByIdQuery();
            query.page = page;
            query.id = id;
            //getChatById.id
            var res =await _mediatr.Send(query);
            return Ok(res);
        }

        // DELETE api/<ChatController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteChatCommand();
            command.id = id;
            var res = await _mediatr.Send(command);

            return Ok(res);
        }
    }
}
