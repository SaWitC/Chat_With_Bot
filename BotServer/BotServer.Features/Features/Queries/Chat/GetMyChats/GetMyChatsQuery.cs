using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Queries.Chat.GetMyChats
{
    public class GetMyChatsQuery : IRequest<IEnumerable<ChatModel>>
    {
        public int Page { get; set; }
        public string Title { get; set; }
        public string AvtorId { get; set; }
    }
}
