using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Remind.DeleteReminder
{
    public class RemoveRemindCommand:IRequest<bool>
    {
        public string AvtorId { get; set; }
        public string ReminderId { get; set; }

    }
}
