using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Queries.Messages.GetMessagesQuery
{
    public class GetMessagesQuery : IRequest<IEnumerable<MessageModel>>
    {
        public string id { get; set; }
        public int page { get; set; }
    }
}
