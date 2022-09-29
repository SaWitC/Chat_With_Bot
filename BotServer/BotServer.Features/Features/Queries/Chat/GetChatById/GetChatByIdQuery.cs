using BotServer.Domain.Models.Details;
using MediatR;

namespace BotServer.Features.Features.Queries.Chat.GetChatById
{
    public class GetChatByIdQuery : IRequest<ChatDetailsModel>
    {
        public string id { get; set; }
    }
}
