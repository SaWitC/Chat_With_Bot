using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Chat.GetChatById
{
    public class GetChatByIdQuery:IRequest<ChatModel>
    {
        public string id { get; set; }
        public int page { get; set; }
    }
}
