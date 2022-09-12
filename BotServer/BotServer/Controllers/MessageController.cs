using BotServer.Features.Features.Commands.Messages.SendMessage;
using BotServer.Features.Features.Queries.Messages.GetMessagesQuery;
using BotServer.SignalR.Hubs;
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
    public class MessageController : ControllerBase
    {
        public Guid Id => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        private readonly IMediator _mediatr;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(IMediator mediator,IHubContext<ChatHub> hubContext)
        {
            _mediatr = mediator;
            _hubContext = hubContext;
        }
        // GET api/<MessageController>/5
        [Authorize(AuthenticationSchemes ="Bearer")]
        [HttpGet("{page}&{chatid}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "get messages", OperationId = "GetMessages")]
        public async Task<IActionResult> Get(int page, string chatid)
        {
            var query = new GetMessagesQuery();
            query.id = chatid;
            query.page = page;
            var res = await _mediatr.Send(query);
            return Ok(res);
        }

       
        // PUT api/<MessageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
