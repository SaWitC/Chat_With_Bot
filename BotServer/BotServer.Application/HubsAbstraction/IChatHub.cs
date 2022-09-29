namespace BotServer.Application.HubsAbstraction
{
    public interface INotifyHub
    {
        public Task OnConnectedAsync();
        public Task OnDisconnectedAsync(Exception? exception);
        public Task SendMessage(string ChatId, string message);
    }
}
