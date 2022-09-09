using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Messages.BotResponse
{
    public class WriteResponseFromServer:IRequest<MessageModel>
    {
        public string MessageFromUser { get; set; }
        public string ChatId { get; set; }
    }
}
