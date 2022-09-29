using MediatR;

namespace BotServer.Features.Features.Commands.Chat.DeleteChatCommand
{
    public class DeleteChatCommand : IRequest<bool>
    {
        public string id { get; set; }
    }
}
