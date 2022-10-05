using BotServer.Application.HubsAbstraction;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BotServer.SignalR_info.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HubForNotify : Hub, INotifyHub
    {

        private readonly IHttpContextAccessor _accesor;
        private readonly IBaseRepository _baseRepository;
        private readonly IHubRepository _hubRepository;
        private readonly IRemindRepository _remindRepository;
        private readonly ILogger<HubForNotify> _logger;
        

        public HubForNotify(
            IHttpContextAccessor accessor,
            ILogger<HubForNotify> logger,
            IHubRepository hubRepository,
            IBaseRepository baseRepository,
            IRemindRepository remindRepository)
        {
            _logger = logger;
            _remindRepository = remindRepository;
            _accesor = accessor;
            _hubRepository = hubRepository;
            _baseRepository = baseRepository;
        }

        public Guid Id => Guid.Parse(_accesor.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public override async Task OnConnectedAsync()
        {
            HubConnections hubConnection = new HubConnections();
            hubConnection.AvtorId = Id.ToString();
            hubConnection.id = Guid.NewGuid().ToString();
            hubConnection.HubConnection = Context.ConnectionId;
            

            hubConnection.IsClosed = false;
            await _baseRepository.Create<HubConnections>(hubConnection);
            await _baseRepository.SaveChangesAsync();
            await base.OnConnectedAsync();

            //BackgroundJob.Schedule(() => SendOld(Context.ConnectionId,Id.ToString()),new TimeSpan(1000));


        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connection = await _hubRepository.GetByHubConnection(Context.ConnectionId);
            if (connection != null)
            {
                var res = _hubRepository.CloseConnectionById(connection.id);
                if (res != null)
                {
                    await _baseRepository.SaveChangesAsync();
                }
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string ChatId, string message)
        {
            await Clients.Client(ChatId).SendAsync("Notify", message);
        }
    }
}
