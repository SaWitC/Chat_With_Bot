using BotServer.Application.DataServices;
using BotServer.Features.Features.Queries.Messages.GetMessagesQuery;
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
    public class MessageController : ControllerBase
    {
        private readonly IHttpContextService _httpContextService;

        private readonly IMediator _mediatr;

        public MessageController(IMediator mediator,IHttpContextService httpContextService)
        {
            _httpContextService = httpContextService;
            _mediatr = mediator;
        }

        [Authorize]
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
    }
}
