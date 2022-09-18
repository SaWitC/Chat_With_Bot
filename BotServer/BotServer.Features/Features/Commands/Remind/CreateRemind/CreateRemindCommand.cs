using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Remind.CreateRemind
{
    public class CreateRemindCommand:IRequest<RemindModel>
    {
        public string RemindMessage { get; set; }
        public DateTime RemindAtTime { get; set; }
    }
}
