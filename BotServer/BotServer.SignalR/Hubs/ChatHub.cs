//using BotServer.Services.Services.Commands;
using BotServer.Application.Repositories;
using BotServer.Application.Services.Commands;
using BotServer.Features.Features.Commands.Messages.SendMessage;
using BotServer.Services.Services.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BotServer.SignalR.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHub : Hub
    {

        private readonly IMediator _mediatr;
        private readonly IEnumerable<ICommandHandler> _commandHandlers;
        private readonly IHttpContextAccessor _accesor;
        private readonly IBaseRepository _baseRepository;
        private readonly IHubRepository _hubRepository;


        public ChatHub(IEnumerable<ICommandHandler> commandHandlers,
            IMediator mediator,
            IHttpContextAccessor accessor,
            IHubRepository hubRepository,
            IBaseRepository baseRepository)
        {
            _commandHandlers = commandHandlers;
            _mediatr = mediator;
            _accesor = accessor;
            _hubRepository = hubRepository;
            _baseRepository = baseRepository;
        }

        public Guid Id => Guid.Parse(_accesor.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public async Task AskServer(string someTextFromClient, string chatId)
        {
            string tempString = "";
            if (string.IsNullOrEmpty(someTextFromClient))
            {
                await Clients.Client(this.Context.ConnectionId).SendAsync("askServerResponse", "incorrect message (empty)");
                return;
            }
            if (someTextFromClient.Length > 400)
            {
                await Clients.Client(this.Context.ConnectionId).SendAsync("askServerResponse", "incorrect message (message>400)");
                return;
            }
            foreach (var handler in _commandHandlers)
            {
                if (handler.CanProcess(new Command(someTextFromClient)))
                {
                    tempString = await handler.ProcessCommand(new Command(someTextFromClient));
                    break;
                }
            }
            //user quest
            SendMessageCommand sendMessageCommand = new SendMessageCommand();
            SendMessageDTO messageDTO = new();
            sendMessageCommand.SendMessageDTO = messageDTO;
            sendMessageCommand.SendMessageDTO.text = someTextFromClient;
            sendMessageCommand.SendMessageDTO.ParentId = chatId;
            sendMessageCommand.SendMessageDTO.avtroId = Id.ToString();
            sendMessageCommand.SendMessageDTO.IsFromBot = false;

            var res = await _mediatr.Send(sendMessageCommand);

            await Clients.Client(this.Context.ConnectionId).SendAsync("messageFromPeople", res.text);

            //server response
            SendMessageCommand sendMessageCommandFromServer = new SendMessageCommand();
            SendMessageDTO BotMessageDTO = new();
            BotMessageDTO.text = tempString;
            BotMessageDTO.ParentId = chatId;
            BotMessageDTO.avtroId = Guid.Empty.ToString();
            BotMessageDTO.IsFromBot = true;
            sendMessageCommandFromServer.SendMessageDTO = BotMessageDTO;
            var botResp = await _mediatr.Send(sendMessageCommandFromServer);

            await Clients.Client(this.Context.ConnectionId).SendAsync("askServerResponse", botResp.text);

            //BackgroundJob.Schedule(() => SendToVk(user, model), delay);

        }
    }
}
