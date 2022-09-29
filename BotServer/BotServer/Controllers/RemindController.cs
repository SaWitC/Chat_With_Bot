
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
        public Guid UserId => Guid.Parse(HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        private readonly IMediator _Mediator;
        public RemindController(IMediator mediator)
        {
            _Mediator = mediator;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetActualAndExpired")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "get reminds", OperationId = "GetActualAndExpiredReminds")]
        public async Task<IActionResult> GetActualAndExpiredReminds()
        {
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                var query = new GetActualAndExpiredRemindsQuery();
                query.AvtorId = UserId.ToString();
                var res = await _Mediator.Send(query);
                return Ok(res);
            }
            return Unauthorized();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("RemoveReminder")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Remove reminder", OperationId = "RemoveReminder")]
        public async Task<IActionResult> RemoveReminder(string Id)
        {
            if (!string.IsNullOrEmpty(Id.ToString()))
            {
                var command = new RemoveRemindCommand();
                command.ReminderId = Id.ToString();
                command.AvtorId = UserId.ToString();
                var res = await _Mediator.Send(command);
                return Ok(res);
            }
            return BadRequest();
        }
    }
}
