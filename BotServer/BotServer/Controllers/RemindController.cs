
using BotServer.Application.DataServices;
using BotServer.Features.Features.Commands.Remind.DeleteReminder;
using BotServer.Features.Features.Queries.Reminds.getActualAndExpiredReminds;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.Swagger;
using System.Security.Claims;

namespace BotServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindController : ControllerBase
    {
        private readonly IHttpContextService _httpContextService;
        private readonly IMediator _Mediator;
        public RemindController(IMediator mediator,IHttpContextService httpContextService)
        {
            _httpContextService = httpContextService;
            _Mediator = mediator;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetActualAndExpired")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "get reminds", OperationId = "GetActualAndExpiredReminds")]
        public async Task<IActionResult> GetActualAndExpiredReminds()
        {
            if (!string.IsNullOrEmpty(_httpContextService.GetCurentUserId()))
            {
                var query = new GetActualAndExpiredRemindsQuery();
                query.AvtorId = _httpContextService.GetCurentUserId();
                var res = await _Mediator.Send(query);
                return Ok(res);
            }
            return Unauthorized();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("RemoveReminder")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Remove reminder", OperationId = "RemoveReminder")]
        public async Task<IActionResult> RemoveReminder(string id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var command = new RemoveRemindCommand();
                command.ReminderId = id.ToString();
                command.AvtorId = _httpContextService.GetCurentUserId();
                var res = await _Mediator.Send(command);
                return Ok(res);
            }
            return BadRequest();
        }
    }
}
