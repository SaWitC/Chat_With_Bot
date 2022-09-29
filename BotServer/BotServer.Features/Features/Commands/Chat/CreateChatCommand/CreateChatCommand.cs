using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Commands.Chat.CreateChatCommand
{
    public class CreateChatCommand : IRequest<ChatModel>
    {
        //public string avtorId { get; set; }

        public string Title { get; set; }
    }
}
