using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Chat.GetMyChats
{
    public class GetMyChatsQuery:IRequest<IEnumerable<ChatModel>>
    {
        public int page { get; set; }
        public string Title { get; set; }
    }
}
