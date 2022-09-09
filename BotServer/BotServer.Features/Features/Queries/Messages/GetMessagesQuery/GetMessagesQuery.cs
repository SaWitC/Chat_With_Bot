using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Messages.GetMessagesQuery
{
    public class GetMessagesQuery:IRequest<IEnumerable<MessageModel>>
    {
        public string id { get; set; }
        public int page { get; set; }
    }
}
