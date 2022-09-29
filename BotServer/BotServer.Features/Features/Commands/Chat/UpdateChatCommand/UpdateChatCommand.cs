using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Commands.Chat.UpdateChatCommand
{
    public class UpdateChatCommand : IRequest<ChatModel>
    {
        public string oldModelId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}
