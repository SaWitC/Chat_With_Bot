using BotServer.Application.HubsAbstraction;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BotServer.SignalR_info.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HubForNotify : Hub, INotifyHub
    {

        private readonly IHttpContextAccessor _accesor;
        private readonly IBaseRepository _baseRepository;
        private readonly IHubRepository _hubRepository;


        public HubForNotify(
            IHttpContextAccessor accessor,
            IHubRepository hubRepository,
            IBaseRepository baseRepository)
        {
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
