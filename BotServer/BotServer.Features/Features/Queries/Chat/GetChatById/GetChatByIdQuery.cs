using BotServer.Domain.Models;
using BotServer.Domain.Models.Details;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Chat.GetChatById
{
    public class GetChatByIdQuery:IRequest<ChatDetailsModel>
    {
        public string id { get; set; }
    }
}
