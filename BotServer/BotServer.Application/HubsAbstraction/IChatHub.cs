using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.HubsAbstraction
{
    public interface IChatHub 
    {
        public Task OnConnectedAsync();

        public Task OnDisconnectedAsync(Exception? exception);
        public Task AskServer(string someTextFromClient, string chatId);

        public Task SendMessage(string ChatId, string message);
    }
}
