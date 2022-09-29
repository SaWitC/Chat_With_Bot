//using BotServer.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BotServer.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        //private readonly IMediator _mediatr;
        //private readonly IHubContext<ChatHub> _hubContext;

        //public SignalRController(IMediator mediatr, IHubContext<ChatHub> hubContext)
        //{
        //    _mediatr = mediatr;
        //    _hubContext = hubContext;
        //}

        //public Guid Id => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        //// POST api/<MessageController>
        //[Route("Send")]
        //[HttpPost]
        ////[Route("Send")]
        ////[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        ////[SwaggerOperation(summary: "send message ", OperationId = "SendMessage")]
        ////[Authorize]
        //public async Task<IActionResult> Post(messageDTo sendMessageModel)
        //{
        //    //var command = new SendMessageCommand();
        //    //command.SendMessageDTO = sendMessageModel;
        //    //var res = await _mediatr.Send(command);
        //    await _hubContext.Clients.All.SendAsync("Notify", sendMessageModel.user,sendMessageModel.msgText);

        //    //return Ok(res);
        //    return Ok();
        //}

    }
    public class messageDTo
    {
        public string user { get; set; }
        public string msgText { get; set; }
    }


}


